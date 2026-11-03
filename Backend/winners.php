<?php
header("Content-Type: application/json");

$mysqli = new mysqli("localhost","root","","examen2");

if ($mysqli->connect_error)
    {
        die("Error de conexion ptm");
    }
else
    {
        $sql = "SELECT final, name FROM ranking WHERE final = 1;";

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