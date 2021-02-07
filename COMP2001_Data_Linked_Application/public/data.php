<html>
    <head>
        <!-- METADATA -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta name="author" content="Tom">

        <!-- Bootstrap CSS -->
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
        <!-- CSS -->
        <link rel="stylesheet" type="text/css" href="./../assets/css/stylesheet.css">
        <!-- Leaflet JS -->
        <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"
              integrity="sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A=="
              crossorigin=""/>
        <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"
                integrity="sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA=="
                crossorigin=""></script>
    </head>

    <body>
        <div class="container-fluid">
            <div class="row" style="height: 100%">
                <div class="col-3 panel-box">
                    <!-- section : Menu -->
                    <?php
                        include('menu.php');
                    ?>
                </div>
                <div class="col-9 content-box">
                    <!-- section : human readable -->
                    <h1 style="color: #000241;">Human Readable</h1>
                    <div class="divider"></div>
                    <div>
                        <p class="para" style="margin-bottom: 30px;">This data section is based of the GeoJSON data provided from the Plymouth Open Data Repository about schools in plymouth. You can find it <a href="https://plymouth.thedata.place/dataset/schools">here</a></p>
                        <h1 style="color: #000241; font-size: 22px; margin-bottom: 15px;">Schools in Plymouth Map</h1>
                        <div class="divider__small"></div>
                        <p class="para">Below is an interactive map of Plymouth that contains markers of every school in the dataset available. You can zoom-in / zoom-out and click on the marker to find which school you're looking at.
                        You can also view the table data below if you wish to view all of the data-points available. Note that some data has been omited due to duplicated geo-coordinates.</p>
                    </div>
                    <!-- section : maps -->
                    <div id="map" style="height: 600px;"></div>
                    <div>
                        <p class="para">*NOTE: The column 'phase' refers to the type of school, for instance, 'p' stands for Primary School or 's' stands for Secondary School</p>
                        <!-- section : data table -->
                        <table class="table">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Phase</th>
                                    <th scope="col">Longitude</th>
                                    <th scope="col">Latitude</th>
                                </tr>
                            </thead>
                            <tbody>
                                <?php
                                    // Generate school data into a table
                                    $originalData = file_get_contents("../data/geojs_original_data.json");
                                    $decode_json = json_decode($originalData, true);
                                    foreach ($decode_json['features'] as $key => $school) {
                                        echo '<tr>';
                                            echo '<th scope="row">' . $key . '</th>';
                                            echo '<td>' . $school['properties']['School name'];
                                            echo '<td>' . $school['properties']['Phase'];
                                            echo '<td>' . $school['properties']['Eastings'];
                                            echo '<td>' . $school['properties']['Northings'];
                                        echo '</tr>';
                                    }
                                ?>
                            </tbody>
                        </table>
                    </div>
                    <!-- section : footer -->
                    <?php

                    include('footer.php');
                    ?>
                </div>
            </div>
        </div>
        <script type="module">
            import OsGridRef from 'https://cdn.jsdelivr.net/npm/geodesy@2/osgridref.js';

            // initialize the map on the "map" div with a given center and zoom
            var map = L.map('map').setView([50.376289, -4.143841], 13);

            // Create tile layer using openstreetmap
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(map);

            // Fetch converted JSON-LD formatted file and convert coordinates to WGS 84 datum type.
            fetch('http://web.socem.plymouth.ac.uk/comp2001/tadams/data/converted_data.json')
                .then(response => response.json())
                .then(data => {
                    // Loop through each schools Eastings / Northings and convert.
                    data['schools'].forEach((element, index) => {
                        const gridRef = new OsGridRef(data['schools'][index]['geo']['latitude'], data['schools'][index]['geo']['longitude']);

                        // Convert GeoCoords from OSGB 1936 to WGS 84 datum type
                        var convertedCoords = gridRef.toLatLon();

                        // Add marker to LeafJS map
                        var marker = L.marker([convertedCoords['_lat'], convertedCoords['_lon']]).addTo(map).bindPopup(data['schools'][index]['name']);
                    })
                })
        </script>
    </body>
</html>

