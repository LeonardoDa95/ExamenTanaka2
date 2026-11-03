<?php
header("Content-Type: application/json");

$mysqli = new mysqli("localhost","root","","examen2");

if ($mysqli->connect_error)
    {
        die("Error de conexion ptm");
    }
else
    {
        $sql = "SELECT name, score FROM ranking ORDER BY score ASC LIMIT 10;";

        $query = $mysqli->prepare($sql);

        $query->execute();

        $result = $query->get_result();

        $data = [
            "data" => $result->fetch_all(MYSQLI_ASSOC)
        ];

        echo json_encode($data);

        $mysqli->close();
    }

?>