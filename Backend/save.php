<?php
header("Content-Type: application/json; charset=UTF-8");

$mysqli = new mysqli("localhost", "root", "", "examen2");

if ($mysqli->connect_error)
{
    echo json_encode(["ok" => false, "error" => "Error de conexion"]);
    exit;
}

$name = strtoupper(trim($_POST["name"] ?? ""));
$score = $_POST["score"] ?? "";
$final = $_POST["final"] ?? "";
$levels = $_POST["levels"] ?? "";

if (!preg_match("/^[A-Z]{1,3}$/", $name))
{
    echo json_encode(["ok" => false, "error" => "Nombre invalido"]);
    exit;
}

if (filter_var($score, FILTER_VALIDATE_INT) === false ||
    (int)$score < 0 ||
    ($final != "0" && $final != "1") ||
    !in_array($levels, ["level 1", "level 2", "level 3"]))
{
    echo json_encode(["ok" => false, "error" => "Datos invalidos"]);
    exit;
}

$sql = "INSERT INTO ranking (name, score, final, levels)
        VALUES (?, ?, ?, ?)";

$query = $mysqli->prepare($sql);
$score = (int)$score;
$final = (int)$final;
$query->bind_param("siis", $name, $score, $final, $levels);

if ($query->execute())
{
    echo json_encode(["ok" => true]);
}
else
{
    echo json_encode(["ok" => false, "error" => "No se pudo guardar"]);
}

$query->close();
$mysqli->close();
?>
