//
//  ContentView.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import SwiftUI
import SwiftUIRefresh
import URLImage

struct ContentView: View {
    @ObservedObject var weatherStore: WeatherStore
    
    @State private var isShowing = false
    private let isoDateFormatter = DateFormatter()
    private let localtimeFormatter = DateFormatter()
    

    var body: some View {
        NavigationView {
            VStack(alignment: .center) {
                HStack {
                    URLImage(URL(string: getUrlFor(icon: weatherStore.weatherViewModel.current.Icon))!)
                    Text(String(format: "%.1f°",
                        weatherStore.weatherViewModel.current.Temperature))
                }
//                Text("\(isoDateToTime(isoDateString: weatherStore.weatherViewModel.current.DateTime) ?? "")")
//                Text(String(format: "Mittlere Temperatur: %.1f°",
//                            weatherStore.weatherViewModel.AverageTemperature))
//                Text(String(format: "Mittlere Temperatur: %.1f°",
//                            weatherStore.weatherViewModel.AverageTemperature))
//                Text(String(format: "Mittlere Feuchte: %.1f %%",
//                            weatherStore.weatherViewModel.AverageHumidity))
                List(weatherStore.weatherViewModel.forecast) { forecastModel in
                    HStack{
                        Text(self.isoDateToTime(isoDateString: forecastModel.DateTime) ?? "")
                        Text(String(format: "%.1f°", forecastModel.Temperature))
                        URLImage(URL(string: "https://openweathermap.org/img/wn/\(forecastModel.Icon).png")!)
                            .padding(.leading, 20)
                    }
                }
                .pullToRefresh(isShowing: $isShowing) {
                    DispatchQueue.main.asyncAfter(deadline: .now() + 1) {
                        self.weatherStore.refresh() // TODO reference-cycle
                        self.isShowing = false
                    }
                }
            } .navigationBarTitle(Text(LocalStorage.GetCurrentCityName() + " " + (isoDateToTime(isoDateString: weatherStore.weatherViewModel.current.DateTime) ?? "")), displayMode: .inline)
        }
    }
    
    init(weatherStore: WeatherStore) {
        self.weatherStore = weatherStore
        isoDateFormatter.timeZone = NSTimeZone(name: "UTC") as TimeZone?
        isoDateFormatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'"
        
        localtimeFormatter.timeZone = NSTimeZone.local
        localtimeFormatter.dateFormat = "dd.MM HH:mm"
    }
    
    private func getUrlFor(icon: String) -> String {
        // When request fails because of empty icon at start of the app
        // The following request with the correct icon doesn't update the Image
        
        let icon = icon == "" ? "03d" : icon
        
        return "https://openweathermap.org/img/wn/\(icon)@2x.png"
    }
    
    private func isoDateToTime(isoDateString: String) -> String? {
        guard let utcDate = isoDateFormatter.date(from: isoDateString) else { return nil }

        return localtimeFormatter.string(from: utcDate)
    }

}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView(weatherStore: WeatherStore(cityId: nil, cityName: nil))
    }
}
