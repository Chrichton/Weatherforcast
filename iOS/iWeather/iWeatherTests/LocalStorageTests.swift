//
//  LocalStorageTests.swift
//  iWeatherTests
//
//  Created by Heiko Goes on 19.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import XCTest

class LocalStorageTests: XCTestCase {
    var cityId: Int?
    
    override func setUpWithError() throws {
        cityId = LocalStorage.getCurrentCityId()
    }

    override func tearDownWithError() throws {
        LocalStorage.set(cityId: cityId)
    }
    
    // TODO should be abstracted. LocalStorage depends directly on UserDefaults
    func testgetCurrentCityId() throws {
        XCTAssertEqual(2911298, LocalStorage.getCurrentCityId())
    }
    
    func testgetCurrentCityName() throws {
        XCTAssertEqual("Hamburg", LocalStorage.GetCurrentCityName())
    }
    
    func testSet() throws {
        LocalStorage.set(cityId: 0815)
        
        XCTAssertEqual(0815, LocalStorage.getCurrentCityId())
    }
    
    func testAdd() throws {
        LocalStorage.add(cityId: 4711, cityName: "42")
        XCTAssertEqual(2911298, LocalStorage.getCurrentCityId())
        XCTAssertEqual("Hamburg", LocalStorage.GetCurrentCityName())
        
        LocalStorage.set(cityId: 4711)
        XCTAssertEqual(4711, LocalStorage.getCurrentCityId())
        XCTAssertEqual("42", LocalStorage.GetCurrentCityName())
    }
}
