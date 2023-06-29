namespace publi_api_tests
{
    public static class SeedEAutoContext
    {
        internal static void SeedDb(EAutoContext context)
        {
            var bodytypes = new List<BodyTypeDataModel>() 
            {
                new("Sedan"),
                new("Liftback"),
                new("Сrossover"),
                new("Minibus"),
                new("Minivan"),
                new("Van"),
                new("SUV"),
                new("Station Wagon"),
            };
            context.BodyTypes.AddRange(bodytypes);

            var brands = new List<BrandDataModel>
            {
                new("Audi"),
                new("Volkswagen"),
                new("Volvo"),
                new("Jaguar"),
                new("Land Rover")
            };
            context.Brands.AddRange(brands);

            var driveTypes = new List<DriveTypeDataModel>()
            {
                new("Rear Wheel Drive"),
                new("Front Wheel Drive"),
                new("All Wheel Drive")
            };
            context.DriveTypes.AddRange(driveTypes);

            var transmissions = new List<TransmissionDataModel>()
            {
                new ("Manual"),
                new ("Automatic")
            };
            context.Transmissions.AddRange(transmissions);

            var models = new List<ModelDataModel>()
            {
                new("XC 90", 3),
                new("XC 60", 3),
                new("V 40", 3),
                new("V 60", 3),
                new("V 90", 3),
                new("S 60", 3),
                new("Q7", 1),
                new("Q5", 1),
                new("Range Rover Sport", 5),
                new("Range Rover Velar", 5),
                new("Range Rover Discovery", 5),
                new("Arteon", 2),
                new("Tiguan", 2),
                new("Touareg", 2),
                new("XE", 4),
                new("F-Pace", 4),
                new("I-Pace", 4),
                new("F-Type", 4),
            };
            context.Models.AddRange(models);

            var generations = new List<GenerationDataModel>()
            {
                new("4M, 2015-2019", 1, 7),
                new("FY, 2016-2020", 1, 8),
                new("I, 2017-2023", 5, 10),
                new("III, 2022...", 5, 9),
                new("II Restyling, 2020...", 2, 13),
                new("III, 2018...", 2, 14),
                new("II Restyling, 2019...", 2, 1),
                new("II, 2016-2020", 3, 5),
                new("I, 2016-2020", 4, 16),
            };
            context.Generations.AddRange(generations);

            var cars = new List<CarDataModel>()
            {
                new(60000,
                "\\images\\cars\\063dc23b-c2e3-4af0-8a8a-85ba67af26d7.jpg",
                Convert.ToDateTime("01.01.2018"),
                DateTime.Now,
                160000,
                "<p>3.0T Quattro,&nbsp;7 places,&nbsp;Top-of-the-range Prestige Premium+, factory antichrome, 21 l / d on almost new all-season tires, Door closers, Panorama with sunroof, Heated and ventilated seats, Heated steering wheel, Multi-zone climate control, Cameras 360, Bose premium audio system, Dead zone sensors</p>",
                null,
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
                2,
                "3.0",
                null,
                1,
                7,
                1,
                7,
                3,
                2),

                new(34700,
                "\\images\\cars\\80a3d6f3-ffee-4e61-933f-7d40056a2508.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                138000,
                "<p>Audi Q5 2.0 TDI quattro S tronic.&nbsp;The car is in excellent condition. Fresh MOT according to the regulations from an authorized dealer in Germany, without a run in the Republic of Belarus. Without painted elements and mileage adjustments.&nbsp;Lane keeping assistant.&nbsp;Adaptive cruise control (ACCS).&nbsp;Rain sensor.&nbsp;Light sensor.&nbsp;Power trunk. Electric turnbuckle. Power front seats.&nbsp;Heated front seats</p>",
                null,
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
                2,
                "2.0",
                null,
                1,
                8,
                2,
                7,
                3,
                2),

                new(225000,
                "\\images\\cars\\1bbfc189-58f2-4f01-ab08-429fef0a5eba.jpg",
                Convert.ToDateTime("01.01.2022"),
                DateTime.Now,
                29,
                "<p>New car. With VAT 20%.</p>\r\n<p>Equipment AUTOBIOGRAPHY</p>\r\n<p>&bull; Heated windshield</p>\r\n<p>&bull; Heated washer jets</p>\r\n<p>&bull; Heated steering wheel</p>\r\n<p>&bull; Autobiography badge</p>\r\n<p>&bull; Electrically adjustable exterior mirrors, heated, memory function, ambient lights and auto-dimming function (driver's side)</p>\r\n<p>&bull; Heated rear window</p>\r\n<p>&bull; Rear window wiper</p>\r\n<p>&bull; Windshield wipers with rain sensors</p>\r\n<p>&bull; Winter parking position of cleaners</p>\r\n<p>&bull; Automatic headlights</p>\r\n<p>&bull; High pressure headlight washers</p>\r\n<p>&bull; Automatic headlight range adjustment</p>\r\n<p>&bull; Rear fog lights</p>",
                null,
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gasoline),
                1,
                "3.0",
                null,
                5,
                9,
                4,
                7,
                3,
                2),

                new(42980,
                "\\images\\cars\\74d95594-aeae-42a1-b565-7ead89e871d0.jpg",
                Convert.ToDateTime("01.01.2020"),
                DateTime.Now,
                40000,
                "<p>With VAT 20%. One owner.</p>\r\n<p>Full service history at office. Volkswagen dealer. Warranty until 02/03/2024.</p>\r\n<p>The car is located on the trading platform Vernye avto at st. Pritytsky 60th century.</p>\r\n<p>We provide a guarantee of legal purity.</p>\r\n<p>Options: keyless entry, digital dashboard, LED light, atmospheric interior lighting, 3-zone climate control, cruise control, distance control, lane control, el. side mirror adjustment, heated mirrors, heated windshield, heated front and rear seats, rear view camera, parking sensors, anti-lock braking system (ABS), brake force distribution (EBD), brake assist system (BAS), electronic stability control (ESP) ).</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gasoline),
                1,
                "2.0",
                null,
                2,
                13,
                5,
                7,
                3,
                2),

                new(59000,
                "\\images\\cars\\67b33629-db15-411c-8b40-d10fde20991a.jpg",
                Convert.ToDateTime("01.01.2018"),
                DateTime.Now,
                59000,
                "<p>Car from Germany. Completely cleared. Without run across RB and RF.</p>\r\n<p>Ideal condition. Original mileage, all elements without colors.</p>\r\n<p>Air suspension, electric hitch, DYNAUDIO music, TRAILER ASSIST, lane keeping.</p>\r\n<p>MOT passed.</p>\r\n<p>Ready to visit the official dealer at your expense.</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
                2,
                "3.0",
                null,
                2,
                14,
                6,
                7,
                3,
                2),

                new(57200,
                "\\images\\cars\\459ff4b0-e7e6-4023-bbfd-cc48812faaf5.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                68000,
                "<p>Volvo XC90 Inscription flagship crossover for sale!</p>\r\n<p>The car was bought in Germany from an authorized dealer Hannover VolvoGmbh (and serviced there).</p>\r\n<p>Restyled 2020 car with the patented Mild HybridVolvo B5, combined with the notorious</p>\r\n<p>\"Power Pulse\" - additional engine agility (only in D5 engines).</p>\r\n<p>Interior: nice perforated Nappa leather, natural wood inserts and black ceiling, seats with ventilation and massage.</p>\r\n<p>The car is in excellent condition both technically and aesthetically. The entire package of documents and service history.</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.DieselGibrid),
                6,
                "2.0",
                null,
                3,
                1,
                7,
                7,
                3,
                2),

                new(43500,
                "\\images\\cars\\ef6b5b97-051f-4cec-b98f-c425774cca85.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                103000,
                "<p>Model year 2020, B4204T34(T8) plug in hybrid engine (can run on both electric and gasoline engines), power 390 hp (303 hp ICE + 87 hp electric motor), acceleration to hundreds of 5.3s, 8-speed automatic transmission Geartronisin Japan, four-wheel drive, double glazing, heated seats (all), ventilation, massage, memory driver, passenger, dashboard in leather, black ceiling, autonomous interior heating, heated steering wheel, autostart and control of all parameters via the Volvo cars app, adaptive cruise, active Pilot, all safety assistants, lane keeping, harman kardon music, electric trunk lid with foot control, headlights FULL LED ACTIVE HIGH BEAM with auto-range and cornering lights, with oncoming traffic cut and much more.</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.GasolineGibrid),
                5,
                "2.0",
                null,
                3,
                5,
                8,
                7,
                3,
                2),

                new(38350,
                "\\images\\cars\\f78b7cbf-948d-4bfd-a037-23a6e2e8449b.jpg",
                Convert.ToDateTime("01.01.2016"),
                DateTime.Now,
                93600,
                "<p>&bull; Car purchased from an Official Dealer</p>\r\n<p>&bull; Complete set of keys</p>\r\n<p>&bull; Service history</p>\r\n<p>Main characteristics:</p>\r\n<p>&bull; Engine: diesel (300 hp)</p>\r\n<p>&bull; Volume 3000 cm3</p>\r\n<p>Transmission: Automatic</p>\r\n<p>Production date: 2016</p>\r\n<p>&bull; Drive: full</p>\r\n<p>&bull; Vin: SADCA2BK5HA092284</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
                2,
                "3.0",
                null,
                4,
                16,
                9,
                7,
                3,
                2)
            };
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}