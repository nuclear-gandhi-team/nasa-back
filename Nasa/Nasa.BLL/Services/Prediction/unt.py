import sys
import requests
import pandas as pd
import pickle
from datetime import datetime, timedelta

request_file_path = sys.argv[1]
# folder_path = '/content/drive/MyDrive/wildfire_forecast'
# file_path = f'{folder_path}/rf_model.pkl'
file_path = '/rf_model.pkl'



def get_weather_df(
    latitude=0,
    longitude=0,
    timestamp="0000-00-00T00:00",
):
    current_time = datetime.now()
    provided_time = datetime.fromisoformat(timestamp)

    if provided_time >= current_time:
        base_url = "https://api.open-meteo.com/v1/forecast"
    else:
        base_url = "https://archive-api.open-meteo.com/v1/archive"

    measurements = [
        'temperature_2m',
        'pressure_msl',
        'surface_pressure',
        'windspeed_10m',
        'winddirection_10m',
        'winddirection_100m',
        'windgusts_10m',
        'soil_temperature_7_to_28cm',
        'soil_temperature_28_to_100cm',
        'soil_moisture_0_to_7cm',
        'soil_moisture_7_to_28cm',
        'soil_moisture_28_to_100cm'
    ]

    params = {
        "latitude": latitude,
        "longitude": longitude,
        "start_date": timestamp.split('T')[0],
        "end_date": timestamp.split('T')[0],
        "hourly": ','.join(measurements)
    }

    response = requests.get(base_url, params=params)

    # Check if request was successful
    if response.status_code != 200:
        print(response)
        return pd.DataFrame(columns=['time'] + measurements)

    data = response.json()

    df = pd.DataFrame(data['hourly'])

    timestamp = datetime.fromisoformat(timestamp)
    timestamp = datetime(timestamp.year, timestamp.month, timestamp.day, timestamp.hour) + timedelta(hours=round(timestamp.minute/60))
    timestamp = timestamp.isoformat()[:-3]

    selected_row = df[df['time'] == timestamp]
    selected_row['latitude'] = latitude
    selected_row['longitude'] = longitude

    return selected_row


dataframes = []
with open(request_file_path, 'r') as file:
    for line in file:
        row_values = line.strip().split(';')
        dataframes.append(get_weather_df(row_values[0], row_values[1], row_values[2]))

  
data = pd.concat(dataframes, axis=0)


def prepare_data(data):
  df_numeric = data.select_dtypes(include=['float64', 'int64'])

  df_numeric_no_duplicates = df_numeric.drop_duplicates()

  df_numeric_no_duplicates_no_nan = df_numeric_no_duplicates.fillna(0)
  df_numeric_no_duplicates_no_nan.shape

  lat_long = ['latitude', 'longitude']
  params_for_results =  data[lat_long]

  needed_features = [
    'temperature_2m',
  'pressure_msl',
  'surface_pressure',
  'windspeed_10m',
  'winddirection_10m',
  'winddirection_100m',
  'windgusts_10m',
  ]

  x = data[needed_features]

  return x, params_for_results

  return x, params_for_results
x, params_for_results = prepare_data(data)


with open(file_path, 'rb') as file:
    loaded_model = pickle.load(file)

results = loaded_model.predict(x)
prediction_series = pd.Series(results, name='prediction')

result_dataset = pd.concat([params_for_results, prediction_series], axis=1)
print(result_dataset)
