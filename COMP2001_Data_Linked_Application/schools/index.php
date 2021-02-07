
<?php
$originalData = file_get_contents("../data/geojs_original_data.json");
$decode_json = json_decode($originalData, true);
echo '{ "@context": {
                    "description": "Linked data showing the schools in Plymouth. Source: ",
                    "schools": "https://schema.org/School",
                    "name": "https://schema.org/name",
                    "phase": "https://schema.org/alternateName",
                    "geo":"https://schema.org/geo",
                    "latitude": {
                        "@id":"https://schema.org/latitude",
                        "@type":"https://schema.org/Number"
                    },
                    "longitude": {
                        "@id":"https://schema.org/longitude",
                        "@type":"https://schema.org/Number"
                    }
                },
                "schools": [';
                foreach ($decode_json['features'] as $key => $school) {
                    echo '{';
                        echo '"name":' . '"' . $school['properties']['School name'] . '"' . ',';
                        echo '"phase":' . '"' . $school['properties']['Phase'] . '"' . ',';
                        echo '"geo":' . '{';
                            echo '"latitude":' . $school['properties']['Eastings'] . ',';
                            echo '"longitude":' . $school['properties']['Northings'];
                        echo '}';
                    end($decode_json['features']);
                    echo $key === key($decode_json['features']) ? '}]}' : '},';
                }
?>
