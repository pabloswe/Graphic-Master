﻿namespace Domain.Exceptions
{
    public class ExceptionMessage
    {
        public const string USER_INVALID_USERNAME = "El Username es incorrecto. No puede ser vacio ni contener numeros o caracteres especiales.";
        public const string USER_INVALID_PASSWORD = "La contrasena es incorrecta. No puede ser vacia y un minimo de caracteres 5 caracteres.";
        public const string USER_INVALID_NAME = "El nombre del usuario es incorrecto. No puede ser vacío ni contener numeros o caracteres especiales.";
        public const string USER_INVALID_SURNAME = "El apellido del usuario es incorrecto. No puede ser vacío ni contener numeros o caracteres especiales.";
        public const string USER_INVALID_ID = "La cédula no puede ser vacía. Debe ser un numero y no debe incluir letras o caracteres especiales.";
        public const string USER_INVALID_ADDRESS = "La Direccion no puede ser vacia. Porfavor ingrese una.";
        public const string USER_INVALID_PHONE = "El Telefono es incorrecto. Debe ser un numero y no debe incluir letras o caracteres especiales.";
        public const string USER_NOT_EXIST = "Ese usuario no existe.";
        public const string USER_ALREADY_EXSIST = "Ese usuario ya existe.";
        public const string USER_INVALID_LOGIN = "El nombre de usuario no coincide con la contraseña.";


        public const string GRID_INVALID_HEIGHT_ABOVE = "La altura es incorrecta. Debe ser menor quer 25 metros.";
        public const string GRID_INVALID_HEIGHT_UNDER = "La altura es incorrecta. Debe ser mayor quer 0 metros.";
        public const string GRID_INVALID_WIDTH_ABOVE = "El ancho es incorrecto. Debe ser menor quer 25 metros.";
        public const string GRID_INVALID_WIDTH_UNDER = "El ancho es incorrecto. Debe ser mayor quer 0 metros.";
        public const string WALL_ALREADY_EXSIST = "La pared ya existe. Dibuje nuevamente.";
        public const string POINT_OUT_OF_WALL = "Inserte la apertura sobre una pared libre.";
        public const string CONTAINED_WALL = "La pared es incorrecta. Dibuje nuevamente.";
    }
}
