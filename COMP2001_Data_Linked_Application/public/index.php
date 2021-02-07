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
                <div class="col-9 content-box_main">
                    <div>
                        <!-- section : welcome -->
                        <div style="margin-bottom: 50px">
                            <h1 style="color: #000241">Welcome</h1>
                            <div class="divider"></div>
                            <p class="para">This application was built on the specification of COMP2001 Part-2. It demonstrates how a data-linked application can be used and
                                its usefulness in the modern web environment. The dataset chosen was based on schools in Plymouth, allowing parents to view
                                the schools around a specific location on a map. You can view the original dataset <a href="https://plymouth.thedata.place/dataset/schools">here</a>.
                            </p>
                        </div>
                        <!-- section : product vision -->
                        <div style="margin-bottom: 50px">
                            <h1 class="headings_small">Product Vision</h1>
                            <div class="divider__small"></div>
                            <p class="para">Plymouth Schools is for parents interested in identifying school types around specific locations in Plymouth, this would enable adults to visualize
                                schools where they could potentially send their children and its surroundings. It is a semantic web application that presents data in two types, human and machine-readable format.
                                It also provides a machine-to-machine formatted file for consumption if required.
                            </p>
                        </div>
                        <!-- section : information -->
                        <div style="margin-bottom: 50px">
                            <h1 class="headings_small">Information</h1>
                            <div class="divider__small"></div>
                            <p class="para"><em> Human Readable: </em>You can view the human readable format of this data, which shows the number of schools, their
                                phase and the GEO location (latitudinal and longitude) in Plymouth. This is also represented on a viewable map
                                that you can see live. You can view this data <a href="data.php">here</a>.
                            </p>
                            <p class="para"><em> Machine Readable: </em>The original dataset was in a geo-JSON format containing every school of all types located in
                                Plymouth. The data has been re-formatted into JSON-LD. You can view this data <a href="../schools/index.php">here</a>.
                            </p>
                        </div>
                    </div>
                    <div>
                        <!-- section : credits -->
                        <div style="margin-bottom: 50px">
                            <h1 style="color: #000241; font-size: 22px">Credits</h1>
                            <ul>
                                <li>
                                    <a href="https://getbootstrap.com/">Bootstrap - Layout of content</a>
                                </li>
                                <li>
                                    <a href="https://leafletjs.com/index.html">LeafletJS - Mapping Tiles and Markers</a>
                                </li>
                                <li>
                                    <a href="https://www.movable-type.co.uk/scripts/geodesy-library.html">Geodesy Converting - OSGB 1936 to WGS 84</a>
                                </li>
                            </ul>
                        </div>
                        <!-- section : footer -->
                        <?php
                            include('footer.php');
                        ?>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>


