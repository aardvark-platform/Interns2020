module Client

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fetch.Types
open Thoth.Fetch
open Fulma
open Thoth.Json

open Shared

let data = 
    """
    Name,Miles per gallon,Cylinders,Engine displacement,Horsepower,Vehicle weight,Acceleration,Model year,Origin,
    chevrolet chevelle malibu,18.0000,8.0000,307.0000,130.0000,3504.0000,12.0000,70.0000,1.0000,
    buick skylark 320,15.0000,8.0000,350.0000,165.0000,3693.0000,11.5000,70.0000,1.0000,
    plymouth satellite,18.0000,8.0000,318.0000,150.0000,3436.0000,11.0000,70.0000,1.0000,
    amc rebel sst,16.0000,8.0000,304.0000,150.0000,3433.0000,12.0000,70.0000,1.0000,
    ford torino,17.0000,8.0000,302.0000,140.0000,3449.0000,10.5000,70.0000,1.0000,
    ford galaxie 500,15.0000,8.0000,429.0000,198.0000,4341.0000,10.0000,70.0000,1.0000,
    chevrolet impala,14.0000,8.0000,454.0000,220.0000,4354.0000,9.0000,70.0000,1.0000,
    plymouth fury iii,14.0000,8.0000,440.0000,215.0000,4312.0000,8.5000,70.0000,1.0000,
    pontiac catalina,14.0000,8.0000,455.0000,225.0000,4425.0000,10.0000,70.0000,1.0000,
    amc ambassador dpl,15.0000,8.0000,390.0000,190.0000,3850.0000,8.5000,70.0000,1.0000,
    citroen ds-21 pallas,,4.0000,133.0000,115.0000,3090.0000,17.5000,70.0000,2.0000,
    chevrolet chevelle concours (sw),,8.0000,350.0000,165.0000,4142.0000,11.5000,70.0000,1.0000,
    ford torino (sw),,8.0000,351.0000,153.0000,4034.0000,11.0000,70.0000,1.0000,
    plymouth satellite (sw),,8.0000,383.0000,175.0000,4166.0000,10.5000,70.0000,1.0000,
    amc rebel sst (sw),,8.0000,360.0000,175.0000,3850.0000,11.0000,70.0000,1.0000,
    dodge challenger se,15.0000,8.0000,383.0000,170.0000,3563.0000,10.0000,70.0000,1.0000,
    plymouth 'cuda 340,14.0000,8.0000,340.0000,160.0000,3609.0000,8.0000,70.0000,1.0000,
    ford mustang boss 302,,8.0000,302.0000,140.0000,3353.0000,8.0000,70.0000,1.0000,
    chevrolet monte carlo,15.0000,8.0000,400.0000,150.0000,3761.0000,9.5000,70.0000,1.0000,
    buick estate wagon (sw),14.0000,8.0000,455.0000,225.0000,3086.0000,10.0000,70.0000,1.0000,
    toyota corona mark ii,24.0000,4.0000,113.0000,95.0000,2372.0000,15.0000,70.0000,3.0000,
    plymouth duster,22.0000,6.0000,198.0000,95.0000,2833.0000,15.5000,70.0000,1.0000,
    amc hornet,18.0000,6.0000,199.0000,97.0000,2774.0000,15.5000,70.0000,1.0000,
    ford maverick,21.0000,6.0000,200.0000,85.0000,2587.0000,16.0000,70.0000,1.0000,
    datsun pl510,27.0000,4.0000,97.0000,88.0000,2130.0000,14.5000,70.0000,3.0000,
    volkswagen 1131 deluxe sedan,26.0000,4.0000,97.0000,46.0000,1835.0000,20.5000,70.0000,2.0000,
    peugeot 504,25.0000,4.0000,110.0000,87.0000,2672.0000,17.5000,70.0000,2.0000,
    audi 100 ls,24.0000,4.0000,107.0000,90.0000,2430.0000,14.5000,70.0000,2.0000,
    saab 99e,25.0000,4.0000,104.0000,95.0000,2375.0000,17.5000,70.0000,2.0000,
    bmw 2002,26.0000,4.0000,121.0000,113.0000,2234.0000,12.5000,70.0000,2.0000,
    amc gremlin,21.0000,6.0000,199.0000,90.0000,2648.0000,15.0000,70.0000,1.0000,
    ford f250,10.0000,8.0000,360.0000,215.0000,4615.0000,14.0000,70.0000,1.0000,
    chevy c20,10.0000,8.0000,307.0000,200.0000,4376.0000,15.0000,70.0000,1.0000,
    dodge d200,11.0000,8.0000,318.0000,210.0000,4382.0000,13.5000,70.0000,1.0000,
    hi 1200d,9.0000,8.0000,304.0000,193.0000,4732.0000,18.5000,70.0000,1.0000,
    datsun pl510,27.0000,4.0000,97.0000,88.0000,2130.0000,14.5000,71.0000,3.0000,
    chevrolet vega 2300,28.0000,4.0000,140.0000,90.0000,2264.0000,15.5000,71.0000,1.0000,
    toyota corona,25.0000,4.0000,113.0000,95.0000,2228.0000,14.0000,71.0000,3.0000,
    ford pinto,25.0000,4.0000,98.0000,,2046.0000,19.0000,71.0000,1.0000,
    volkswagen super beetle 117,,4.0000,97.0000,48.0000,1978.0000,20.0000,71.0000,2.0000,
    amc gremlin,19.0000,6.0000,232.0000,100.0000,2634.0000,13.0000,71.0000,1.0000,
    plymouth satellite custom,16.0000,6.0000,225.0000,105.0000,3439.0000,15.5000,71.0000,1.0000,
    chevrolet chevelle malibu,17.0000,6.0000,250.0000,100.0000,3329.0000,15.5000,71.0000,1.0000,
    ford torino 500,19.0000,6.0000,250.0000,88.0000,3302.0000,15.5000,71.0000,1.0000,
    amc matador,18.0000,6.0000,232.0000,100.0000,3288.0000,15.5000,71.0000,1.0000,
    chevrolet impala,14.0000,8.0000,350.0000,165.0000,4209.0000,12.0000,71.0000,1.0000,
    pontiac catalina brougham,14.0000,8.0000,400.0000,175.0000,4464.0000,11.5000,71.0000,1.0000,
    ford galaxie 500,14.0000,8.0000,351.0000,153.0000,4154.0000,13.5000,71.0000,1.0000,
    plymouth fury iii,14.0000,8.0000,318.0000,150.0000,4096.0000,13.0000,71.0000,1.0000,
    dodge monaco (sw),12.0000,8.0000,383.0000,180.0000,4955.0000,11.5000,71.0000,1.0000,
    ford country squire (sw),13.0000,8.0000,400.0000,170.0000,4746.0000,12.0000,71.0000,1.0000,
    pontiac safari (sw),13.0000,8.0000,400.0000,175.0000,5140.0000,12.0000,71.0000,1.0000,
    amc hornet sportabout (sw),18.0000,6.0000,258.0000,110.0000,2962.0000,13.5000,71.0000,1.0000,
    chevrolet vega (sw),22.0000,4.0000,140.0000,72.0000,2408.0000,19.0000,71.0000,1.0000,
    pontiac firebird,19.0000,6.0000,250.0000,100.0000,3282.0000,15.0000,71.0000,1.0000,
    ford mustang,18.0000,6.0000,250.0000,88.0000,3139.0000,14.5000,71.0000,1.0000,
    mercury capri 2000,23.0000,4.0000,122.0000,86.0000,2220.0000,14.0000,71.0000,1.0000,
    opel 1900,28.0000,4.0000,116.0000,90.0000,2123.0000,14.0000,71.0000,2.0000,
    peugeot 304,30.0000,4.0000,79.0000,70.0000,2074.0000,19.5000,71.0000,2.0000,
    fiat 124b,30.0000,4.0000,88.0000,76.0000,2065.0000,14.5000,71.0000,2.0000,
    toyota corolla 1200,31.0000,4.0000,71.0000,65.0000,1773.0000,19.0000,71.0000,3.0000,
    datsun 1200,35.0000,4.0000,72.0000,69.0000,1613.0000,18.0000,71.0000,3.0000,
    volkswagen model 111,27.0000,4.0000,97.0000,60.0000,1834.0000,19.0000,71.0000,2.0000,
    plymouth cricket,26.0000,4.0000,91.0000,70.0000,1955.0000,20.5000,71.0000,1.0000,
    toyota corona hardtop,24.0000,4.0000,113.0000,95.0000,2278.0000,15.5000,72.0000,3.0000,
    dodge colt hardtop,25.0000,4.0000,97.5000,80.0000,2126.0000,17.0000,72.0000,1.0000,
    volkswagen type 3,23.0000,4.0000,97.0000,54.0000,2254.0000,23.5000,72.0000,2.0000,
    chevrolet vega,20.0000,4.0000,140.0000,90.0000,2408.0000,19.5000,72.0000,1.0000,
    ford pinto runabout,21.0000,4.0000,122.0000,86.0000,2226.0000,16.5000,72.0000,1.0000,
    chevrolet impala,13.0000,8.0000,350.0000,165.0000,4274.0000,12.0000,72.0000,1.0000,
    pontiac catalina,14.0000,8.0000,400.0000,175.0000,4385.0000,12.0000,72.0000,1.0000,
    plymouth fury iii,15.0000,8.0000,318.0000,150.0000,4135.0000,13.5000,72.0000,1.0000,
    ford galaxie 500,14.0000,8.0000,351.0000,153.0000,4129.0000,13.0000,72.0000,1.0000,
    amc ambassador sst,17.0000,8.0000,304.0000,150.0000,3672.0000,11.5000,72.0000,1.0000,
    mercury marquis,11.0000,8.0000,429.0000,208.0000,4633.0000,11.0000,72.0000,1.0000,
    buick lesabre custom,13.0000,8.0000,350.0000,155.0000,4502.0000,13.5000,72.0000,1.0000,
    oldsmobile delta 88 royale,12.0000,8.0000,350.0000,160.0000,4456.0000,13.5000,72.0000,1.0000,
    chrysler newport royal,13.0000,8.0000,400.0000,190.0000,4422.0000,12.5000,72.0000,1.0000,
    mazda rx2 coupe,19.0000,3.0000,70.0000,97.0000,2330.0000,13.5000,72.0000,3.0000,
    amc matador (sw),15.0000,8.0000,304.0000,150.0000,3892.0000,12.5000,72.0000,1.0000,
    chevrolet chevelle concours (sw),13.0000,8.0000,307.0000,130.0000,4098.0000,14.0000,72.0000,1.0000,
    ford gran torino (sw),13.0000,8.0000,302.0000,140.0000,4294.0000,16.0000,72.0000,1.0000,
    plymouth satellite custom (sw),14.0000,8.0000,318.0000,150.0000,4077.0000,14.0000,72.0000,1.0000,
    volvo 145e (sw),18.0000,4.0000,121.0000,112.0000,2933.0000,14.5000,72.0000,2.0000,
    volkswagen 411 (sw),22.0000,4.0000,121.0000,76.0000,2511.0000,18.0000,72.0000,2.0000,
    peugeot 504 (sw),21.0000,4.0000,120.0000,87.0000,2979.0000,19.5000,72.0000,2.0000,
    renault 12 (sw),26.0000,4.0000,96.0000,69.0000,2189.0000,18.0000,72.0000,2.0000,
    ford pinto (sw),22.0000,4.0000,122.0000,86.0000,2395.0000,16.0000,72.0000,1.0000,
    datsun 510 (sw),28.0000,4.0000,97.0000,92.0000,2288.0000,17.0000,72.0000,3.0000,
    toyouta corona mark ii (sw),23.0000,4.0000,120.0000,97.0000,2506.0000,14.5000,72.0000,3.0000,
    dodge colt (sw),28.0000,4.0000,98.0000,80.0000,2164.0000,15.0000,72.0000,1.0000,
    toyota corolla 1600 (sw),27.0000,4.0000,97.0000,88.0000,2100.0000,16.5000,72.0000,3.0000,
    buick century 350,13.0000,8.0000,350.0000,175.0000,4100.0000,13.0000,73.0000,1.0000,
    amc matador,14.0000,8.0000,304.0000,150.0000,3672.0000,11.5000,73.0000,1.0000,
    chevrolet malibu,13.0000,8.0000,350.0000,145.0000,3988.0000,13.0000,73.0000,1.0000,
    ford gran torino,14.0000,8.0000,302.0000,137.0000,4042.0000,14.5000,73.0000,1.0000,
    dodge coronet custom,15.0000,8.0000,318.0000,150.0000,3777.0000,12.5000,73.0000,1.0000,
    mercury marquis brougham,12.0000,8.0000,429.0000,198.0000,4952.0000,11.5000,73.0000,1.0000,
    chevrolet caprice classic,13.0000,8.0000,400.0000,150.0000,4464.0000,12.0000,73.0000,1.0000,
    ford ltd,13.0000,8.0000,351.0000,158.0000,4363.0000,13.0000,73.0000,1.0000,
    plymouth fury gran sedan,14.0000,8.0000,318.0000,150.0000,4237.0000,14.5000,73.0000,1.0000,
    chrysler new yorker brougham,13.0000,8.0000,440.0000,215.0000,4735.0000,11.0000,73.0000,1.0000,
    buick electra 225 custom,12.0000,8.0000,455.0000,225.0000,4951.0000,11.0000,73.0000,1.0000,
    amc ambassador brougham,13.0000,8.0000,360.0000,175.0000,3821.0000,11.0000,73.0000,1.0000,
    plymouth valiant,18.0000,6.0000,225.0000,105.0000,3121.0000,16.5000,73.0000,1.0000,
    chevrolet nova custom,16.0000,6.0000,250.0000,100.0000,3278.0000,18.0000,73.0000,1.0000,
    amc hornet,18.0000,6.0000,232.0000,100.0000,2945.0000,16.0000,73.0000,1.0000,
    ford maverick,18.0000,6.0000,250.0000,88.0000,3021.0000,16.5000,73.0000,1.0000,
    plymouth duster,23.0000,6.0000,198.0000,95.0000,2904.0000,16.0000,73.0000,1.0000,
    volkswagen super beetle,26.0000,4.0000,97.0000,46.0000,1950.0000,21.0000,73.0000,2.0000,
    chevrolet impala,11.0000,8.0000,400.0000,150.0000,4997.0000,14.0000,73.0000,1.0000,
    ford country,12.0000,8.0000,400.0000,167.0000,4906.0000,12.5000,73.0000,1.0000,
    plymouth custom suburb,13.0000,8.0000,360.0000,170.0000,4654.0000,13.0000,73.0000,1.0000,
    oldsmobile vista cruiser,12.0000,8.0000,350.0000,180.0000,4499.0000,12.5000,73.0000,1.0000,
    amc gremlin,18.0000,6.0000,232.0000,100.0000,2789.0000,15.0000,73.0000,1.0000,
    toyota carina,20.0000,4.0000,97.0000,88.0000,2279.0000,19.0000,73.0000,3.0000,
    chevrolet vega,21.0000,4.0000,140.0000,72.0000,2401.0000,19.5000,73.0000,1.0000,
    datsun 610,22.0000,4.0000,108.0000,94.0000,2379.0000,16.5000,73.0000,3.0000,
    maxda rx3,18.0000,3.0000,70.0000,90.0000,2124.0000,13.5000,73.0000,3.0000,
    ford pinto,19.0000,4.0000,122.0000,85.0000,2310.0000,18.5000,73.0000,1.0000,
    mercury capri v6,21.0000,6.0000,155.0000,107.0000,2472.0000,14.0000,73.0000,1.0000,
    fiat 124 sport coupe,26.0000,4.0000,98.0000,90.0000,2265.0000,15.5000,73.0000,2.0000,
    chevrolet monte carlo s,15.0000,8.0000,350.0000,145.0000,4082.0000,13.0000,73.0000,1.0000,
    pontiac grand prix,16.0000,8.0000,400.0000,230.0000,4278.0000,9.5000,73.0000,1.0000,
    fiat 128,29.0000,4.0000,68.0000,49.0000,1867.0000,19.5000,73.0000,2.0000,
    opel manta,24.0000,4.0000,116.0000,75.0000,2158.0000,15.5000,73.0000,2.0000,
    audi 100ls,20.0000,4.0000,114.0000,91.0000,2582.0000,14.0000,73.0000,2.0000,
    volvo 144ea,19.0000,4.0000,121.0000,112.0000,2868.0000,15.5000,73.0000,2.0000,
    dodge dart custom,15.0000,8.0000,318.0000,150.0000,3399.0000,11.0000,73.0000,1.0000,
    saab 99le,24.0000,4.0000,121.0000,110.0000,2660.0000,14.0000,73.0000,2.0000,
    toyota mark ii,20.0000,6.0000,156.0000,122.0000,2807.0000,13.5000,73.0000,3.0000,
    oldsmobile omega,11.0000,8.0000,350.0000,180.0000,3664.0000,11.0000,73.0000,1.0000,
    plymouth duster,20.0000,6.0000,198.0000,95.0000,3102.0000,16.5000,74.0000,1.0000,
    ford maverick,21.0000,6.0000,200.0000,,2875.0000,17.0000,74.0000,1.0000,
    amc hornet,19.0000,6.0000,232.0000,100.0000,2901.0000,16.0000,74.0000,1.0000,
    chevrolet nova,15.0000,6.0000,250.0000,100.0000,3336.0000,17.0000,74.0000,1.0000,
    datsun b210,31.0000,4.0000,79.0000,67.0000,1950.0000,19.0000,74.0000,3.0000,
    ford pinto,26.0000,4.0000,122.0000,80.0000,2451.0000,16.5000,74.0000,1.0000,
    toyota corolla 1200,32.0000,4.0000,71.0000,65.0000,1836.0000,21.0000,74.0000,3.0000,
    chevrolet vega,25.0000,4.0000,140.0000,75.0000,2542.0000,17.0000,74.0000,1.0000,
    chevrolet chevelle malibu classic,16.0000,6.0000,250.0000,100.0000,3781.0000,17.0000,74.0000,1.0000,
    amc matador,16.0000,6.0000,258.0000,110.0000,3632.0000,18.0000,74.0000,1.0000,
    plymouth satellite sebring,18.0000,6.0000,225.0000,105.0000,3613.0000,16.5000,74.0000,1.0000,
    ford gran torino,16.0000,8.0000,302.0000,140.0000,4141.0000,14.0000,74.0000,1.0000,
    buick century luxus (sw),13.0000,8.0000,350.0000,150.0000,4699.0000,14.5000,74.0000,1.0000,
    dodge coronet custom (sw),14.0000,8.0000,318.0000,150.0000,4457.0000,13.5000,74.0000,1.0000,
    ford gran torino (sw),14.0000,8.0000,302.0000,140.0000,4638.0000,16.0000,74.0000,1.0000,
    amc matador (sw),14.0000,8.0000,304.0000,150.0000,4257.0000,15.5000,74.0000,1.0000,
    audi fox,29.0000,4.0000,98.0000,83.0000,2219.0000,16.5000,74.0000,2.0000,
    volkswagen dasher,26.0000,4.0000,79.0000,67.0000,1963.0000,15.5000,74.0000,2.0000,
    opel manta,26.0000,4.0000,97.0000,78.0000,2300.0000,14.5000,74.0000,2.0000,
    toyota corona,31.0000,4.0000,76.0000,52.0000,1649.0000,16.5000,74.0000,3.0000,
    datsun 710,32.0000,4.0000,83.0000,61.0000,2003.0000,19.0000,74.0000,3.0000,
    dodge colt,28.0000,4.0000,90.0000,75.0000,2125.0000,14.5000,74.0000,1.0000,
    fiat 128,24.0000,4.0000,90.0000,75.0000,2108.0000,15.5000,74.0000,2.0000,
    fiat 124 tc,26.0000,4.0000,116.0000,75.0000,2246.0000,14.0000,74.0000,2.0000,
    honda civic,24.0000,4.0000,120.0000,97.0000,2489.0000,15.0000,74.0000,3.0000,
    subaru,26.0000,4.0000,108.0000,93.0000,2391.0000,15.5000,74.0000,3.0000,
    fiat x1.9,31.0000,4.0000,79.0000,67.0000,2000.0000,16.0000,74.0000,2.0000,
    plymouth valiant custom,19.0000,6.0000,225.0000,95.0000,3264.0000,16.0000,75.0000,1.0000,
    chevrolet nova,18.0000,6.0000,250.0000,105.0000,3459.0000,16.0000,75.0000,1.0000,
    mercury monarch,15.0000,6.0000,250.0000,72.0000,3432.0000,21.0000,75.0000,1.0000,
    ford maverick,15.0000,6.0000,250.0000,72.0000,3158.0000,19.5000,75.0000,1.0000,
    pontiac catalina,16.0000,8.0000,400.0000,170.0000,4668.0000,11.5000,75.0000,1.0000,
    chevrolet bel air,15.0000,8.0000,350.0000,145.0000,4440.0000,14.0000,75.0000,1.0000,
    plymouth grand fury,16.0000,8.0000,318.0000,150.0000,4498.0000,14.5000,75.0000,1.0000,
    ford ltd,14.0000,8.0000,351.0000,148.0000,4657.0000,13.5000,75.0000,1.0000,
    buick century,17.0000,6.0000,231.0000,110.0000,3907.0000,21.0000,75.0000,1.0000,
    chevroelt chevelle malibu,16.0000,6.0000,250.0000,105.0000,3897.0000,18.5000,75.0000,1.0000,
    amc matador,15.0000,6.0000,258.0000,110.0000,3730.0000,19.0000,75.0000,1.0000,
    plymouth fury,18.0000,6.0000,225.0000,95.0000,3785.0000,19.0000,75.0000,1.0000,
    buick skyhawk,21.0000,6.0000,231.0000,110.0000,3039.0000,15.0000,75.0000,1.0000,
    chevrolet monza 2+2,20.0000,8.0000,262.0000,110.0000,3221.0000,13.5000,75.0000,1.0000,
    ford mustang ii,13.0000,8.0000,302.0000,129.0000,3169.0000,12.0000,75.0000,1.0000,
    toyota corolla,29.0000,4.0000,97.0000,75.0000,2171.0000,16.0000,75.0000,3.0000,
    ford pinto,23.0000,4.0000,140.0000,83.0000,2639.0000,17.0000,75.0000,1.0000,
    amc gremlin,20.0000,6.0000,232.0000,100.0000,2914.0000,16.0000,75.0000,1.0000,
    pontiac astro,23.0000,4.0000,140.0000,78.0000,2592.0000,18.5000,75.0000,1.0000,
    toyota corona,24.0000,4.0000,134.0000,96.0000,2702.0000,13.5000,75.0000,3.0000,
    volkswagen dasher,25.0000,4.0000,90.0000,71.0000,2223.0000,16.5000,75.0000,2.0000,
    datsun 710,24.0000,4.0000,119.0000,97.0000,2545.0000,17.0000,75.0000,3.0000,
    ford pinto,18.0000,6.0000,171.0000,97.0000,2984.0000,14.5000,75.0000,1.0000,
    volkswagen rabbit,29.0000,4.0000,90.0000,70.0000,1937.0000,14.0000,75.0000,2.0000,
    amc pacer,19.0000,6.0000,232.0000,90.0000,3211.0000,17.0000,75.0000,1.0000,
    audi 100ls,23.0000,4.0000,115.0000,95.0000,2694.0000,15.0000,75.0000,2.0000,
    peugeot 504,23.0000,4.0000,120.0000,88.0000,2957.0000,17.0000,75.0000,2.0000,
    volvo 244dl,22.0000,4.0000,121.0000,98.0000,2945.0000,14.5000,75.0000,2.0000,
    saab 99le,25.0000,4.0000,121.0000,115.0000,2671.0000,13.5000,75.0000,2.0000,
    honda civic cvcc,33.0000,4.0000,91.0000,53.0000,1795.0000,17.5000,75.0000,3.0000,
    fiat 131,28.0000,4.0000,107.0000,86.0000,2464.0000,15.5000,76.0000,2.0000,
    opel 1900,25.0000,4.0000,116.0000,81.0000,2220.0000,16.9000,76.0000,2.0000,
    capri ii,25.0000,4.0000,140.0000,92.0000,2572.0000,14.9000,76.0000,1.0000,
    dodge colt,26.0000,4.0000,98.0000,79.0000,2255.0000,17.7000,76.0000,1.0000,
    renault 12tl,27.0000,4.0000,101.0000,83.0000,2202.0000,15.3000,76.0000,2.0000,
    chevrolet chevelle malibu classic,17.5000,8.0000,305.0000,140.0000,4215.0000,13.0000,76.0000,1.0000,
    dodge coronet brougham,16.0000,8.0000,318.0000,150.0000,4190.0000,13.0000,76.0000,1.0000,
    amc matador,15.5000,8.0000,304.0000,120.0000,3962.0000,13.9000,76.0000,1.0000,
    ford gran torino,14.5000,8.0000,351.0000,152.0000,4215.0000,12.8000,76.0000,1.0000,
    plymouth valiant,22.0000,6.0000,225.0000,100.0000,3233.0000,15.4000,76.0000,1.0000,
    chevrolet nova,22.0000,6.0000,250.0000,105.0000,3353.0000,14.5000,76.0000,1.0000,
    ford maverick,24.0000,6.0000,200.0000,81.0000,3012.0000,17.6000,76.0000,1.0000,
    amc hornet,22.5000,6.0000,232.0000,90.0000,3085.0000,17.6000,76.0000,1.0000,
    chevrolet chevette,29.0000,4.0000,85.0000,52.0000,2035.0000,22.2000,76.0000,1.0000,
    chevrolet woody,24.5000,4.0000,98.0000,60.0000,2164.0000,22.1000,76.0000,1.0000,
    vw rabbit,29.0000,4.0000,90.0000,70.0000,1937.0000,14.2000,76.0000,2.0000,
    honda civic,33.0000,4.0000,91.0000,53.0000,1795.0000,17.4000,76.0000,3.0000,
    dodge aspen se,20.0000,6.0000,225.0000,100.0000,3651.0000,17.7000,76.0000,1.0000,
    ford granada ghia,18.0000,6.0000,250.0000,78.0000,3574.0000,21.0000,76.0000,1.0000,
    pontiac ventura sj,18.5000,6.0000,250.0000,110.0000,3645.0000,16.2000,76.0000,1.0000,
    amc pacer d/l,17.5000,6.0000,258.0000,95.0000,3193.0000,17.8000,76.0000,1.0000,
    volkswagen rabbit,29.5000,4.0000,97.0000,71.0000,1825.0000,12.2000,76.0000,2.0000,
    datsun b-210,32.0000,4.0000,85.0000,70.0000,1990.0000,17.0000,76.0000,3.0000,
    toyota corolla,28.0000,4.0000,97.0000,75.0000,2155.0000,16.4000,76.0000,3.0000,
    ford pinto,26.5000,4.0000,140.0000,72.0000,2565.0000,13.6000,76.0000,1.0000,
    volvo 245,20.0000,4.0000,130.0000,102.0000,3150.0000,15.7000,76.0000,2.0000,
    plymouth volare premier v8,13.0000,8.0000,318.0000,150.0000,3940.0000,13.2000,76.0000,1.0000,
    peugeot 504,19.0000,4.0000,120.0000,88.0000,3270.0000,21.9000,76.0000,2.0000,
    toyota mark ii,19.0000,6.0000,156.0000,108.0000,2930.0000,15.5000,76.0000,3.0000,
    mercedes-benz 280s,16.5000,6.0000,168.0000,120.0000,3820.0000,16.7000,76.0000,2.0000,
    cadillac seville,16.5000,8.0000,350.0000,180.0000,4380.0000,12.1000,76.0000,1.0000,
    chevy c10,13.0000,8.0000,350.0000,145.0000,4055.0000,12.0000,76.0000,1.0000,
    ford f108,13.0000,8.0000,302.0000,130.0000,3870.0000,15.0000,76.0000,1.0000,
    dodge d100,13.0000,8.0000,318.0000,150.0000,3755.0000,14.0000,76.0000,1.0000,
    honda accord cvcc,31.5000,4.0000,98.0000,68.0000,2045.0000,18.5000,77.0000,3.0000,
    buick opel isuzu deluxe,30.0000,4.0000,111.0000,80.0000,2155.0000,14.8000,77.0000,1.0000,
    renault 5 gtl,36.0000,4.0000,79.0000,58.0000,1825.0000,18.6000,77.0000,2.0000,
    plymouth arrow gs,25.5000,4.0000,122.0000,96.0000,2300.0000,15.5000,77.0000,1.0000,
    datsun f-10 hatchback,33.5000,4.0000,85.0000,70.0000,1945.0000,16.8000,77.0000,3.0000,
    chevrolet caprice classic,17.5000,8.0000,305.0000,145.0000,3880.0000,12.5000,77.0000,1.0000,
    oldsmobile cutlass supreme,17.0000,8.0000,260.0000,110.0000,4060.0000,19.0000,77.0000,1.0000,
    dodge monaco brougham,15.5000,8.0000,318.0000,145.0000,4140.0000,13.7000,77.0000,1.0000,
    mercury cougar brougham,15.0000,8.0000,302.0000,130.0000,4295.0000,14.9000,77.0000,1.0000,
    chevrolet concours,17.5000,6.0000,250.0000,110.0000,3520.0000,16.4000,77.0000,1.0000,
    buick skylark,20.5000,6.0000,231.0000,105.0000,3425.0000,16.9000,77.0000,1.0000,
    plymouth volare custom,19.0000,6.0000,225.0000,100.0000,3630.0000,17.7000,77.0000,1.0000,
    ford granada,18.5000,6.0000,250.0000,98.0000,3525.0000,19.0000,77.0000,1.0000,
    pontiac grand prix lj,16.0000,8.0000,400.0000,180.0000,4220.0000,11.1000,77.0000,1.0000,
    chevrolet monte carlo landau,15.5000,8.0000,350.0000,170.0000,4165.0000,11.4000,77.0000,1.0000,
    chrysler cordoba,15.5000,8.0000,400.0000,190.0000,4325.0000,12.2000,77.0000,1.0000,
    ford thunderbird,16.0000,8.0000,351.0000,149.0000,4335.0000,14.5000,77.0000,1.0000,
    volkswagen rabbit custom,29.0000,4.0000,97.0000,78.0000,1940.0000,14.5000,77.0000,2.0000,
    pontiac sunbird coupe,24.5000,4.0000,151.0000,88.0000,2740.0000,16.0000,77.0000,1.0000,
    toyota corolla liftback,26.0000,4.0000,97.0000,75.0000,2265.0000,18.2000,77.0000,3.0000,
    ford mustang ii 2+2,25.5000,4.0000,140.0000,89.0000,2755.0000,15.8000,77.0000,1.0000,
    chevrolet chevette,30.5000,4.0000,98.0000,63.0000,2051.0000,17.0000,77.0000,1.0000,
    dodge colt m/m,33.5000,4.0000,98.0000,83.0000,2075.0000,15.9000,77.0000,1.0000,
    subaru dl,30.0000,4.0000,97.0000,67.0000,1985.0000,16.4000,77.0000,3.0000,
    volkswagen dasher,30.5000,4.0000,97.0000,78.0000,2190.0000,14.1000,77.0000,2.0000,
    datsun 810,22.0000,6.0000,146.0000,97.0000,2815.0000,14.5000,77.0000,3.0000,
    bmw 320i,21.5000,4.0000,121.0000,110.0000,2600.0000,12.8000,77.0000,2.0000,
    mazda rx-4,21.5000,3.0000,80.0000,110.0000,2720.0000,13.5000,77.0000,3.0000,
    volkswagen rabbit custom diesel,43.1000,4.0000,90.0000,48.0000,1985.0000,21.5000,78.0000,2.0000,
    ford fiesta,36.1000,4.0000,98.0000,66.0000,1800.0000,14.4000,78.0000,1.0000,
    mazda glc deluxe,32.8000,4.0000,78.0000,52.0000,1985.0000,19.4000,78.0000,3.0000,
    datsun b210 gx,39.4000,4.0000,85.0000,70.0000,2070.0000,18.6000,78.0000,3.0000,
    honda civic cvcc,36.1000,4.0000,91.0000,60.0000,1800.0000,16.4000,78.0000,3.0000,
    oldsmobile cutlass salon brougham,19.9000,8.0000,260.0000,110.0000,3365.0000,15.5000,78.0000,1.0000,
    dodge diplomat,19.4000,8.0000,318.0000,140.0000,3735.0000,13.2000,78.0000,1.0000,
    mercury monarch ghia,20.2000,8.0000,302.0000,139.0000,3570.0000,12.8000,78.0000,1.0000,
    pontiac phoenix lj,19.2000,6.0000,231.0000,105.0000,3535.0000,19.2000,78.0000,1.0000,
    chevrolet malibu,20.5000,6.0000,200.0000,95.0000,3155.0000,18.2000,78.0000,1.0000,
    ford fairmont (auto),20.2000,6.0000,200.0000,85.0000,2965.0000,15.8000,78.0000,1.0000,
    ford fairmont (man),25.1000,4.0000,140.0000,88.0000,2720.0000,15.4000,78.0000,1.0000,
    plymouth volare,20.5000,6.0000,225.0000,100.0000,3430.0000,17.2000,78.0000,1.0000,
    amc concord,19.4000,6.0000,232.0000,90.0000,3210.0000,17.2000,78.0000,1.0000,
    buick century special,20.6000,6.0000,231.0000,105.0000,3380.0000,15.8000,78.0000,1.0000,
    mercury zephyr,20.8000,6.0000,200.0000,85.0000,3070.0000,16.7000,78.0000,1.0000,
    dodge aspen,18.6000,6.0000,225.0000,110.0000,3620.0000,18.7000,78.0000,1.0000,
    amc concord d/l,18.1000,6.0000,258.0000,120.0000,3410.0000,15.1000,78.0000,1.0000,
    chevrolet monte carlo landau,19.2000,8.0000,305.0000,145.0000,3425.0000,13.2000,78.0000,1.0000,
    buick regal sport coupe (turbo),17.7000,6.0000,231.0000,165.0000,3445.0000,13.4000,78.0000,1.0000,
    ford futura,18.1000,8.0000,302.0000,139.0000,3205.0000,11.2000,78.0000,1.0000,
    dodge magnum xe,17.5000,8.0000,318.0000,140.0000,4080.0000,13.7000,78.0000,1.0000,
    chevrolet chevette,30.0000,4.0000,98.0000,68.0000,2155.0000,16.5000,78.0000,1.0000,
    toyota corona,27.5000,4.0000,134.0000,95.0000,2560.0000,14.2000,78.0000,3.0000,
    datsun 510,27.2000,4.0000,119.0000,97.0000,2300.0000,14.7000,78.0000,3.0000,
    dodge omni,30.9000,4.0000,105.0000,75.0000,2230.0000,14.5000,78.0000,1.0000,
    toyota celica gt liftback,21.1000,4.0000,134.0000,95.0000,2515.0000,14.8000,78.0000,3.0000,
    plymouth sapporo,23.2000,4.0000,156.0000,105.0000,2745.0000,16.7000,78.0000,1.0000,
    oldsmobile starfire sx,23.8000,4.0000,151.0000,85.0000,2855.0000,17.6000,78.0000,1.0000,
    datsun 200-sx,23.9000,4.0000,119.0000,97.0000,2405.0000,14.9000,78.0000,3.0000,
    audi 5000,20.3000,5.0000,131.0000,103.0000,2830.0000,15.9000,78.0000,2.0000,
    volvo 264gl,17.0000,6.0000,163.0000,125.0000,3140.0000,13.6000,78.0000,2.0000,
    saab 99gle,21.6000,4.0000,121.0000,115.0000,2795.0000,15.7000,78.0000,2.0000,
    peugeot 604sl,16.2000,6.0000,163.0000,133.0000,3410.0000,15.8000,78.0000,2.0000,
    volkswagen scirocco,31.5000,4.0000,89.0000,71.0000,1990.0000,14.9000,78.0000,2.0000,
    honda accord lx,29.5000,4.0000,98.0000,68.0000,2135.0000,16.6000,78.0000,3.0000,
    pontiac lemans v6,21.5000,6.0000,231.0000,115.0000,3245.0000,15.4000,79.0000,1.0000,
    mercury zephyr 6,19.8000,6.0000,200.0000,85.0000,2990.0000,18.2000,79.0000,1.0000,
    ford fairmont 4,22.3000,4.0000,140.0000,88.0000,2890.0000,17.3000,79.0000,1.0000,
    amc concord dl 6,20.2000,6.0000,232.0000,90.0000,3265.0000,18.2000,79.0000,1.0000,
    dodge aspen 6,20.6000,6.0000,225.0000,110.0000,3360.0000,16.6000,79.0000,1.0000,
    chevrolet caprice classic,17.0000,8.0000,305.0000,130.0000,3840.0000,15.4000,79.0000,1.0000,
    ford ltd landau,17.6000,8.0000,302.0000,129.0000,3725.0000,13.4000,79.0000,1.0000,
    mercury grand marquis,16.5000,8.0000,351.0000,138.0000,3955.0000,13.2000,79.0000,1.0000,
    dodge st. regis,18.2000,8.0000,318.0000,135.0000,3830.0000,15.2000,79.0000,1.0000,
    buick estate wagon (sw),16.9000,8.0000,350.0000,155.0000,4360.0000,14.9000,79.0000,1.0000,
    ford country squire (sw),15.5000,8.0000,351.0000,142.0000,4054.0000,14.3000,79.0000,1.0000,
    chevrolet malibu classic (sw),19.2000,8.0000,267.0000,125.0000,3605.0000,15.0000,79.0000,1.0000,
    chrysler lebaron town @ country (sw),18.5000,8.0000,360.0000,150.0000,3940.0000,13.0000,79.0000,1.0000,
    vw rabbit custom,31.9000,4.0000,89.0000,71.0000,1925.0000,14.0000,79.0000,2.0000,
    maxda glc deluxe,34.1000,4.0000,86.0000,65.0000,1975.0000,15.2000,79.0000,3.0000,
    dodge colt hatchback custom,35.7000,4.0000,98.0000,80.0000,1915.0000,14.4000,79.0000,1.0000,
    amc spirit dl,27.4000,4.0000,121.0000,80.0000,2670.0000,15.0000,79.0000,1.0000,
    mercedes benz 300d,25.4000,5.0000,183.0000,77.0000,3530.0000,20.1000,79.0000,2.0000,
    cadillac eldorado,23.0000,8.0000,350.0000,125.0000,3900.0000,17.4000,79.0000,1.0000,
    peugeot 504,27.2000,4.0000,141.0000,71.0000,3190.0000,24.8000,79.0000,2.0000,
    oldsmobile cutlass salon brougham,23.9000,8.0000,260.0000,90.0000,3420.0000,22.2000,79.0000,1.0000,
    plymouth horizon,34.2000,4.0000,105.0000,70.0000,2200.0000,13.2000,79.0000,1.0000,
    plymouth horizon tc3,34.5000,4.0000,105.0000,70.0000,2150.0000,14.9000,79.0000,1.0000,
    datsun 210,31.8000,4.0000,85.0000,65.0000,2020.0000,19.2000,79.0000,3.0000,
    fiat strada custom,37.3000,4.0000,91.0000,69.0000,2130.0000,14.7000,79.0000,2.0000,
    buick skylark limited,28.4000,4.0000,151.0000,90.0000,2670.0000,16.0000,79.0000,1.0000,
    chevrolet citation,28.8000,6.0000,173.0000,115.0000,2595.0000,11.3000,79.0000,1.0000,
    oldsmobile omega brougham,26.8000,6.0000,173.0000,115.0000,2700.0000,12.9000,79.0000,1.0000,
    pontiac phoenix,33.5000,4.0000,151.0000,90.0000,2556.0000,13.2000,79.0000,1.0000,
    vw rabbit,41.5000,4.0000,98.0000,76.0000,2144.0000,14.7000,80.0000,2.0000,
    toyota corolla tercel,38.1000,4.0000,89.0000,60.0000,1968.0000,18.8000,80.0000,3.0000,
    chevrolet chevette,32.1000,4.0000,98.0000,70.0000,2120.0000,15.5000,80.0000,1.0000,
    datsun 310,37.2000,4.0000,86.0000,65.0000,2019.0000,16.4000,80.0000,3.0000,
    chevrolet citation,28.0000,4.0000,151.0000,90.0000,2678.0000,16.5000,80.0000,1.0000,
    ford fairmont,26.4000,4.0000,140.0000,88.0000,2870.0000,18.1000,80.0000,1.0000,
    amc concord,24.3000,4.0000,151.0000,90.0000,3003.0000,20.1000,80.0000,1.0000,
    dodge aspen,19.1000,6.0000,225.0000,90.0000,3381.0000,18.7000,80.0000,1.0000,
    audi 4000,34.3000,4.0000,97.0000,78.0000,2188.0000,15.8000,80.0000,2.0000,
    toyota corona liftback,29.8000,4.0000,134.0000,90.0000,2711.0000,15.5000,80.0000,3.0000,
    mazda 626,31.3000,4.0000,120.0000,75.0000,2542.0000,17.5000,80.0000,3.0000,
    datsun 510 hatchback,37.0000,4.0000,119.0000,92.0000,2434.0000,15.0000,80.0000,3.0000,
    toyota corolla,32.2000,4.0000,108.0000,75.0000,2265.0000,15.2000,80.0000,3.0000,
    mazda glc,46.6000,4.0000,86.0000,65.0000,2110.0000,17.9000,80.0000,3.0000,
    dodge colt,27.9000,4.0000,156.0000,105.0000,2800.0000,14.4000,80.0000,1.0000,
    datsun 210,40.8000,4.0000,85.0000,65.0000,2110.0000,19.2000,80.0000,3.0000,
    vw rabbit c (diesel),44.3000,4.0000,90.0000,48.0000,2085.0000,21.7000,80.0000,2.0000,
    vw dasher (diesel),43.4000,4.0000,90.0000,48.0000,2335.0000,23.7000,80.0000,2.0000,
    audi 5000s (diesel),36.4000,5.0000,121.0000,67.0000,2950.0000,19.9000,80.0000,2.0000,
    mercedes-benz 240d,30.0000,4.0000,146.0000,67.0000,3250.0000,21.8000,80.0000,2.0000,
    honda civic 1500 gl,44.6000,4.0000,91.0000,67.0000,1850.0000,13.8000,80.0000,3.0000,
    renault lecar deluxe,40.9000,4.0000,85.0000,,1835.0000,17.3000,80.0000,2.0000,
    subaru dl,33.8000,4.0000,97.0000,67.0000,2145.0000,18.0000,80.0000,3.0000,
    vokswagen rabbit,29.8000,4.0000,89.0000,62.0000,1845.0000,15.3000,80.0000,2.0000,
    datsun 280-zx,32.7000,6.0000,168.0000,132.0000,2910.0000,11.4000,80.0000,3.0000,
    mazda rx-7 gs,23.7000,3.0000,70.0000,100.0000,2420.0000,12.5000,80.0000,3.0000,
    triumph tr7 coupe,35.0000,4.0000,122.0000,88.0000,2500.0000,15.1000,80.0000,2.0000,
    ford mustang cobra,23.6000,4.0000,140.0000,,2905.0000,14.3000,80.0000,1.0000,
    honda accord,32.4000,4.0000,107.0000,72.0000,2290.0000,17.0000,80.0000,3.0000,
    plymouth reliant,27.2000,4.0000,135.0000,84.0000,2490.0000,15.7000,81.0000,1.0000,
    buick skylark,26.6000,4.0000,151.0000,84.0000,2635.0000,16.4000,81.0000,1.0000,
    dodge aries wagon (sw),25.8000,4.0000,156.0000,92.0000,2620.0000,14.4000,81.0000,1.0000,
    chevrolet citation,23.5000,6.0000,173.0000,110.0000,2725.0000,12.6000,81.0000,1.0000,
    plymouth reliant,30.0000,4.0000,135.0000,84.0000,2385.0000,12.9000,81.0000,1.0000,
    toyota starlet,39.1000,4.0000,79.0000,58.0000,1755.0000,16.9000,81.0000,3.0000,
    plymouth champ,39.0000,4.0000,86.0000,64.0000,1875.0000,16.4000,81.0000,1.0000,
    honda civic 1300,35.1000,4.0000,81.0000,60.0000,1760.0000,16.1000,81.0000,3.0000,
    subaru,32.3000,4.0000,97.0000,67.0000,2065.0000,17.8000,81.0000,3.0000,
    datsun 210 mpg,37.0000,4.0000,85.0000,65.0000,1975.0000,19.4000,81.0000,3.0000,
    toyota tercel,37.7000,4.0000,89.0000,62.0000,2050.0000,17.3000,81.0000,3.0000,
    mazda glc 4,34.1000,4.0000,91.0000,68.0000,1985.0000,16.0000,81.0000,3.0000,
    plymouth horizon 4,34.7000,4.0000,105.0000,63.0000,2215.0000,14.9000,81.0000,1.0000,
    ford escort 4w,34.4000,4.0000,98.0000,65.0000,2045.0000,16.2000,81.0000,1.0000,
    ford escort 2h,29.9000,4.0000,98.0000,65.0000,2380.0000,20.7000,81.0000,1.0000,
    volkswagen jetta,33.0000,4.0000,105.0000,74.0000,2190.0000,14.2000,81.0000,2.0000,
    renault 18i,34.5000,4.0000,100.0000,,2320.0000,15.8000,81.0000,2.0000,
    honda prelude,33.7000,4.0000,107.0000,75.0000,2210.0000,14.4000,81.0000,3.0000,
    toyota corolla,32.4000,4.0000,108.0000,75.0000,2350.0000,16.8000,81.0000,3.0000,
    datsun 200sx,32.9000,4.0000,119.0000,100.0000,2615.0000,14.8000,81.0000,3.0000,
    mazda 626,31.6000,4.0000,120.0000,74.0000,2635.0000,18.3000,81.0000,3.0000,
    peugeot 505s turbo diesel,28.1000,4.0000,141.0000,80.0000,3230.0000,20.4000,81.0000,2.0000,
    saab 900s,,4.0000,121.0000,110.0000,2800.0000,15.4000,81.0000,2.0000,
    volvo diesel,30.7000,6.0000,145.0000,76.0000,3160.0000,19.6000,81.0000,2.0000,
    toyota cressida,25.4000,6.0000,168.0000,116.0000,2900.0000,12.6000,81.0000,3.0000,
    datsun 810 maxima,24.2000,6.0000,146.0000,120.0000,2930.0000,13.8000,81.0000,3.0000,
    buick century,22.4000,6.0000,231.0000,110.0000,3415.0000,15.8000,81.0000,1.0000,
    oldsmobile cutlass ls,26.6000,8.0000,350.0000,105.0000,3725.0000,19.0000,81.0000,1.0000,
    ford granada gl,20.2000,6.0000,200.0000,88.0000,3060.0000,17.1000,81.0000,1.0000,
    chrysler lebaron salon,17.6000,6.0000,225.0000,85.0000,3465.0000,16.6000,81.0000,1.0000,
    chevrolet cavalier,28.0000,4.0000,112.0000,88.0000,2605.0000,19.6000,82.0000,1.0000,
    chevrolet cavalier wagon,27.0000,4.0000,112.0000,88.0000,2640.0000,18.6000,82.0000,1.0000,
    chevrolet cavalier 2-door,34.0000,4.0000,112.0000,88.0000,2395.0000,18.0000,82.0000,1.0000,
    pontiac j2000 se hatchback,31.0000,4.0000,112.0000,85.0000,2575.0000,16.2000,82.0000,1.0000,
    dodge aries se,29.0000,4.0000,135.0000,84.0000,2525.0000,16.0000,82.0000,1.0000,
    pontiac phoenix,27.0000,4.0000,151.0000,90.0000,2735.0000,18.0000,82.0000,1.0000,
    ford fairmont futura,24.0000,4.0000,140.0000,92.0000,2865.0000,16.4000,82.0000,1.0000,
    amc concord dl,23.0000,4.0000,151.0000,,3035.0000,20.5000,82.0000,1.0000,
    volkswagen rabbit l,36.0000,4.0000,105.0000,74.0000,1980.0000,15.3000,82.0000,2.0000,
    mazda glc custom l,37.0000,4.0000,91.0000,68.0000,2025.0000,18.2000,82.0000,3.0000,
    mazda glc custom,31.0000,4.0000,91.0000,68.0000,1970.0000,17.6000,82.0000,3.0000,
    plymouth horizon miser,38.0000,4.0000,105.0000,63.0000,2125.0000,14.7000,82.0000,1.0000,
    mercury lynx l,36.0000,4.0000,98.0000,70.0000,2125.0000,17.3000,82.0000,1.0000,
    nissan stanza xe,36.0000,4.0000,120.0000,88.0000,2160.0000,14.5000,82.0000,3.0000,
    honda accord,36.0000,4.0000,107.0000,75.0000,2205.0000,14.5000,82.0000,3.0000,
    toyota corolla,34.0000,4.0000,108.0000,70.0000,2245.0000,16.9000,82.0000,3.0000,
    honda civic,38.0000,4.0000,91.0000,67.0000,1965.0000,15.0000,82.0000,3.0000,
    honda civic (auto),32.0000,4.0000,91.0000,67.0000,1965.0000,15.7000,82.0000,3.0000,
    datsun 310 gx,38.0000,4.0000,91.0000,67.0000,1995.0000,16.2000,82.0000,3.0000,
    buick century limited,25.0000,6.0000,181.0000,110.0000,2945.0000,16.4000,82.0000,1.0000,
    oldsmobile cutlass ciera (diesel),38.0000,6.0000,262.0000,85.0000,3015.0000,17.0000,82.0000,1.0000,
    chrysler lebaron medallion,26.0000,4.0000,156.0000,92.0000,2585.0000,14.5000,82.0000,1.0000,
    ford granada l,22.0000,6.0000,232.0000,112.0000,2835.0000,14.7000,82.0000,1.0000,
    toyota celica gt,32.0000,4.0000,144.0000,96.0000,2665.0000,13.9000,82.0000,3.0000,
    dodge charger 2.2,36.0000,4.0000,135.0000,84.0000,2370.0000,13.0000,82.0000,1.0000,
    chevrolet camaro,27.0000,4.0000,151.0000,90.0000,2950.0000,17.3000,82.0000,1.0000,
    ford mustang gl,27.0000,4.0000,140.0000,86.0000,2790.0000,15.6000,82.0000,1.0000,
    vw pickup,44.0000,4.0000,97.0000,52.0000,2130.0000,24.6000,82.0000,2.0000,
    dodge rampage,32.0000,4.0000,135.0000,84.0000,2295.0000,11.6000,82.0000,1.0000,
    ford ranger,28.0000,4.0000,120.0000,79.0000,2625.0000,18.6000,82.0000,1.0000,
    chevy s-10,31.0000,4.0000,119.0000,82.0000,2720.0000,19.4000,82.0000,1.0000,
    """

// The model holds data that you want to keep track of while the application is running
// in this case, we are keeping track of a counter
// we mark it as optional, because initially it will not be available from the client
// the initial value will be requested from server
type Model = { Counter: Counter option }

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
    | Increment
    | Decrement
    | InitialCountLoaded of Counter

let initialCounter () = Fetch.fetchAs<unit, Counter> "/api/init"

// defines the initial state and initial command (= side-effect) of the application
let init () : Model * Cmd<Msg> =
    let initialModel = { Counter = None }
    let loadCountCmd =
        Cmd.OfPromise.perform initialCounter () InitialCountLoaded
    initialModel, loadCountCmd

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.Counter, msg with
    | Some counter, Increment ->
        let nextModel = { currentModel with Counter = Some { Value = counter.Value + 1 } }
        nextModel, Cmd.none
    | Some counter, Decrement ->
        let nextModel = { currentModel with Counter = Some { Value = counter.Value - 1 } }
        nextModel, Cmd.none
    | _, InitialCountLoaded initialCount->
        let nextModel = { Counter = Some initialCount }
        nextModel, Cmd.none
    | _ -> currentModel, Cmd.none


let safeComponents =
    let components =
        span [ ]
           [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
               [ str "SAFE  "
                 str Version.template ]
             str ", "
             a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
             str ", "
             a [ Href "http://fable.io" ] [ str "Fable" ]
             str ", "
             a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
             str ", "
             a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]

           ]

    span [ ]
        [ str "Version "
          strong [ ] [ str Version.app ]
          str " powered by: "
          components ]

let show = function
    | { Counter = Some counter } -> string counter.Value
    | { Counter = None   } -> "Loading..."

let button txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]

let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "SAFE Template" ] ] ]

          Container.container []
              [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ Heading.h3 [] [ str ("Press buttons to manipulate counter: " + show model) ] ]
                Columns.columns []
                    [ Column.column [] [ button "-" (fun _ -> dispatch Decrement) ]
                      Column.column [] [ button "+" (fun _ -> dispatch Increment) ] ] ]

          Footer.footer [ ]
                [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ safeComponents ] ] ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
