﻿using Nasa.DAL.Entities.Common;

namespace Nasa.DAL.Entities;

public class Subscription: BaseEntity<int>
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Coordinates { get; set; } = string.Empty;
}