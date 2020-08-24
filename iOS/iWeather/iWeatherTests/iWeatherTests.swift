//
//  iWeatherTests.swift
//  iWeatherTests
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import XCTest
@testable import iWeather

class iWeatherTests: XCTestCase {

    override func setUpWithError() throws {
        // Put setup code here. This method is called before the invocation of each test method in the class.
    }

    override func tearDownWithError() throws {
        // Put teardown code here. This method is called after the invocation of each test method in the class.
    }

    func testDecodeWeatherViewModel() throws {
        let jsonFilename = Bundle(for: type(of: self))
            .path(forResource: "weather", ofType: "json")!
        
        let data = NSData(contentsOfFile: jsonFilename)! as Data
        let viewModel = try JSONDecoder().decode(WeatherViewModel.self, from: data)
    
        XCTAssertEqual(14.634, viewModel.AverageTemperature)
        XCTAssertEqual(82.275, viewModel.AverageHumidity)
        
        XCTAssertEqual(13.89, viewModel.current.Temperature)
        XCTAssertEqual(13.86, viewModel.current.FeelsLikeTemperature)
        XCTAssertEqual(12.78, viewModel.current.MinimumTemperature)
        XCTAssertEqual(14.44, viewModel.current.MaximumTemperature)
        XCTAssertEqual(100, viewModel.current.Humidity)
        XCTAssertEqual(1011, viewModel.current.Pressure)
        XCTAssertEqual(1.79, viewModel.current.Windspeed)
        XCTAssertEqual(180, viewModel.current.WindDirection)
        XCTAssertEqual("Überwiegend bewölkt", viewModel.current.CloudDescription)
        XCTAssertEqual("2020-08-24T04:00:14Z", viewModel.current.DateTime)
        XCTAssertEqual("04n", viewModel.current.Icon)
        
        XCTAssertEqual(40, viewModel.forecast.count)
    }

    func testDecodeCity() throws {
        let jsonFilename = Bundle(for: type(of: self))
            .path(forResource: "cities", ofType: "json")!
        
        let data = NSData(contentsOfFile: jsonFilename)! as Data
        let cities = try JSONDecoder().decode([City].self, from: data)
        
        XCTAssertEqual(20, cities.count)
        
        let city = cities[0]
        XCTAssertEqual("Abentheuer", city.Key)
        XCTAssertEqual(2959841, city.Value)
    }
    
//    func testPerformanceExample() throws {
//        // This is an example of a performance test case.
//        self.measure {
//            // Put the code you want to measure the time of here.
//        }
//    }

}
