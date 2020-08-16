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
    
        XCTAssertEqual(20.973751, viewModel.AverageTemperature)
        XCTAssertEqual(70.225, viewModel.AverageHumidity)
    }

//    func testPerformanceExample() throws {
//        // This is an example of a performance test case.
//        self.measure {
//            // Put the code you want to measure the time of here.
//        }
//    }

}
