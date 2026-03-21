// SIMULADOR DE DECISIONES PARA PLATAFORMA DE STREAMING

int totalEvaluados = 0;
int publicados = 0;
int rechazados = 0;
int enRevision = 0;

int impactoAlto = 0;
int impactoMedio = 0;
int impactoBajo = 0;

int opcion;

do
{
    Console.WriteLine("===== SISTEMA DE EVALUACION DE CONTENIDO ======");
    Console.WriteLine("1. Evaluar nuevo contenido");
    Console.WriteLine("2. Mostrar reglas del sistema");
    Console.WriteLine("3. Mostrar estadisticas ");
    Console.WriteLine("4. Reiniciar estadisticas");
    Console.WriteLine("5. Salir");

    Console.Write("Presione uno de los 5 números para seleccionar una opción:");

    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {

        case 1:
            {
                EvaluarContenido();
                break;
            }

        case 2:
            {
                MostrarReglas();
                break;
            }

        case 3:
            {

                break;
            }

        case 4:
            {
                break;
            }

        case 5:
            {
                Console.WriteLine("Resumen Final de la decisión");
            }

            break;


        default:
            {


                break;
            }


    }



       

} while (opcion != 5);

void EvaluarContenido()
{
    Console.WriteLine("#======= Evaluar Tipo de Contenido ==========#");

    Console.WriteLine("Ingrese el Tipo de contenido: (película, serie, documental, evento en vivo): ");
    string tipo = Console.ReadLine().ToLower();


    Console.Write("Ingrese la Duración en minutos: ");
    int duracion = int.Parse(Console.ReadLine());

    Console.Write("Ingrese la Clasificación (tp, +13, +18): ");
    string clasificacion = Console.ReadLine().ToLower();
 
    Console.Write("Ingrese la Hora Programada (0-23):");
    int hora = int.Parse(Console.ReadLine());

    Console.Write("Ingrese el Nivel de Producción: (bajo, medio, alto)");
    string produccion = Console.ReadLine().ToLower();

    (bool valido, string mensaje) = ValidacionTecnica(tipo, duracion, clasificacion, hora, produccion);

    string impacto = "N/A";
    string decision= "";

    if (!valido)
    {
        Console.WriteLine(mensaje);
        decision = "Rechazar";
        rechazados++;
    }

    else
    {
        impacto = ClasificacionImpacto(produccion, duracion, hora);

        if (impacto == "Alto")
        {
            decision = "Enviar revisión";
            enRevision++;
        }
        else
        {
            decision = "Publicar";
            publicados++;
        }

    }

    totalEvaluados++;
    Console.WriteLine("==== RESULTADOS =====");
    Console.WriteLine("Impacto: "+ impacto);
    Console.WriteLine("Decisión: "+ decision);
}



(bool,string) ValidacionTecnica(string tipo, int duracion, string clasificacion, int hora, string produccion)
{
    if (tipo == "pelicula" && (duracion < 60 || duracion > 180))
        return (false, "ERROR!!! Duracion invalida para la película");


    if (tipo == "serie" && (duracion < 20 || duracion > 90))
        return (false, "ERROR!!! Duracion invalida para la serie");


    if (tipo == "documental" && (duracion < 30 || duracion > 120))
        return (false, "ERROR!!! Duracion invalida para el documental");


    if (tipo == "evento" && (duracion < 30 || duracion > 240))
        return (false, "ERROR!!! Duracion invalida para el evento");


    if (clasificacion == "+13" && (hora < 6 || hora > 22))
        return (false, "ERROR!!! Horario no permitido para  +13");


    if (clasificacion == "+18" && !(hora >= 22|| hora <=5))
        return (false, "ERROR!!! Horario no permitido para +18");


    if (produccion == "bajo" &&  clasificacion == "+18")
        return (false, "ERROR!!! Producción no permitida para +18");

    return (true, "Contenido valido");
}

string ClasificacionImpacto(string produccion, int duracion, int hora)
{
    string impacto = "Bajo";

    if (produccion == "alto" || duracion > 120 || (hora >=20 && hora <=23))
    {
        impacto = "Alto";
        impactoAlto++;
    }

    else if (produccion == "medio" || (duracion >= 60 && duracion <= 120))
    {
        impacto = "Medio";
        impactoMedio++;
    }

    else
    {
        impacto = "Bajo";
        impactoBajo++;  

    }
    return impacto;
}

void MostrarReglas()
    {
    Console.WriteLine("Cargando reglas...");

    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(".");
    }

    Console.WriteLine("===== REGLAS DEL SISTEMA =====");

    Console.WriteLine(" ===== Duración por tipo =====");
    Console.WriteLine("Todo público: cualqer hora");
    Console.WriteLine("+13: entre 6 y 22");
    Console.WriteLine("+18: entre 22 y 5");

    Console.WriteLine("                   ");

    Console.WriteLine("=====  Duración por Tipo  =======");
    Console.WriteLine("Película: 60-180 min");
    Console.WriteLine("Serie: 20-90 min");
    Console.WriteLine("Documental: 30-120 min");
    Console.WriteLine("Evento: 30-240 min");
    Console.WriteLine("                   ");
    Console.WriteLine("                   ");

}

void MostrarEstadisticas()
{
    

}

void ReiniciarEstadisticas()
{

}