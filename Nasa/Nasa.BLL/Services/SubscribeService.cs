using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nasa.BLL.Exceptions;
using Nasa.BLL.Services.Abstract;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Subscribe;
using Nasa.DAL.Context;
using Nasa.DAL.Entities;

namespace Nasa.BLL.Services;

public class SubscribeService : BaseService, ISubscribeService
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserService _userService;

    public SubscribeService(NasaContext context, IMapper mapper, IUserIdGetter userIdGetter, IUserService userService) :
        base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
    }

    public async Task<List<SubscribeDto>> GetAllUserSubscription()
    {
        return _mapper.Map<List<SubscribeDto>>(await _context.Subscriptions
            .Where(s => s.Id == _userIdGetter.GetCurrentUserId())
            .ToListAsync());
    }

    public async Task<List<SubscribeDto>> GetAllSubscriptions()
    {
        return _mapper.Map<List<SubscribeDto>>(await _context.Subscriptions.Include(s => s.User).ToListAsync());
    }

    public async Task SubscribeAsync(SubscribeDto subscribeDto)
    {
        var subscribe = _mapper.Map<Subscription>(subscribeDto);
        subscribe.User = await _userService.GetUserByIdAsync(_userIdGetter.GetCurrentUserId());

        _context.Subscriptions.Add(subscribe);
        await _context.SaveChangesAsync();
    }

    public async Task UnsubscribeAsync(SubscribeDto subscribeDto)
    {
        var subscribe = await _context.Subscriptions
            .FirstOrDefaultAsync(s =>
                s.Id == _userIdGetter.GetCurrentUserId() && s.Coordinates == subscribeDto.Coordinates);

        if (subscribe is null)
        {
            throw new NotFoundException(nameof(Subscription));
        }

        _context.Remove(subscribe);
        await _context.SaveChangesAsync();
    }
}