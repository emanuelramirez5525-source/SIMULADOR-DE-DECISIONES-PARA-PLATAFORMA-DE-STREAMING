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
    Console.Clear();

    Console.WriteLine("===== SISTEMA DE EVALUACION DE CONTENIDO ======");
    Console.WriteLine("1. Evaluar nuevo contenido");
    Console.WriteLine("2. Mostrar reglas del sistema");
    Console.WriteLine("3. Mostrar estadisticas ");
    Console.WriteLine("4. Reiniciar estadisticas");
    Console.WriteLine("5. Salir");

    Console.Write("Seleccione una opción: ");

    while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 5)
    {
        Console.WriteLine("Porfavor, Ingrese una opción válida (1-5): ");
    }

    switch (opcion)
    {

        case 1:
            {
                Console.Clear ();
                EvaluarContenido();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey(true);
                break;
            }

        case 2:
            {
                Console.Clear();
                MostrarReglas();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey(true);
                break;
            }

        case 3:
            {
                Console.Clear();
                MostrarEstadisticas();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey(true);
                break;
            }

        case 4:
            {
                Console.Clear();
                ReiniciarEstadisticas();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey(true);
                break;
            }

        case 5:
            {

                Console.Clear();
                Console.WriteLine(" #==== RESUMEN FINAL ====# ");
                MostrarEstadisticas();
            }

            break;
    }     

} while (opcion != 5);


bool ControladorIntentos(ref int intentos)
{
    intentos++;
    if (intentos == 5)
    {
        Console.WriteLine("Demasiados intentos...");
        Console.WriteLine("El formulario se reinciara..");
        Console.WriteLine("Presione una tecla...");
        Console.ReadKey(true);

        Console.Clear();

        intentos = 0;
        return true;

    }
  return false;
}

void EvaluarContenido()
{
    Console.WriteLine("#======= Evaluar Tipo de Contenido ==========#");
    Console.WriteLine("(Si se equivoca mas de 5 veces, La opción evaluar contenido se reiniciara)");

    string tipo;
    int intentosTipo = 0;

    do
    {
        Console.WriteLine("Ingrese el Tipo de contenido: (pelicula, serie, documental, evento): ");
        tipo = Console.ReadLine().ToLower();

        if (tipo != "pelicula" && tipo != "serie" && tipo != "documental" && tipo != "evento")
        {
            Console.WriteLine("Tipo invalido...");

            if (ControladorIntentos(ref intentosTipo)) return;
        }

    } while (tipo != "pelicula" && tipo != "serie" && tipo != "documental" && tipo != "evento" );


    int duracion;
    int intentosDuración = 0;

    Console.Write("Ingrese la Duración en minutos: ");

    while (!int.TryParse(Console.ReadLine(), out duracion) || duracion <=0)
    {
        Console.WriteLine("Porfavor ingrese un número valido o mayor a 0: ");

        if (ControladorIntentos(ref intentosDuración)) return;
    }

    string clasificacion;
    int intentosClasificacion = 0;

    do
    {
        Console.Write("Ingrese la Clasificación (tp, +13, +18): ");
        clasificacion = Console.ReadLine().ToLower();

        if (clasificacion != "tp" && clasificacion != "+13" && clasificacion != "+18")
        {
            Console.WriteLine("Clasificacíón invalida");

            if (ControladorIntentos (ref intentosClasificacion)) return;

        }


    } while (clasificacion != "tp" && clasificacion != "+13" && clasificacion != "+18");


    int hora;
    int intentosHora = 0;   
    Console.Write("Ingrese la Hora Programada (0-23): ");

    while (!int.TryParse(Console.ReadLine(), out hora ) || hora < 0 || hora > 23)
    {
        Console.WriteLine("Porfavor, ingrese un valor valido (0-23)");

        if (ControladorIntentos(ref intentosHora)) return;
    }

    string produccion;
    int intentosProduccion = 0;
    do
    {
        Console.Write("Ingrese el Nivel de Producción: (bajo, medio, alto): ");
        produccion = Console.ReadLine().ToLower();

        if (produccion != "bajo" && produccion != "medio" && produccion != "alto")
        {
            if (ControladorIntentos(ref intentosProduccion)) return;
        }


    } while (produccion != "bajo" && produccion != "medio" && produccion != "alto");
    

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
            decision = "Enviar a revisión";
            enRevision++;
        }
        else
        {
            decision = "Publicar";
            publicados++;
        }

    }

    totalEvaluados++;
    Console.Clear();
    Console.WriteLine("#==== RESULTADOS =====#");
    Console.WriteLine("Impacto: "+ impacto);
    Console.WriteLine("Decisión: "+ decision);
}



(bool,string) ValidacionTecnica(string tipo, int duracion, string clasificacion, int hora, string produccion)
{
    if (tipo == "pelicula" && (duracion < 60 || duracion > 180))
        return (false, "ERROR!!! Duracion inválida para la película");


    if (tipo == "serie" && (duracion < 20 || duracion > 90))
        return (false, "ERROR!!! Duracion inválida para la serie");


    if (tipo == "documental" && (duracion < 30 || duracion > 120))
        return (false, "ERROR!!! Duracion inválida para el documental");


    if (tipo == "evento" && (duracion < 30 || duracion > 240))
        return (false, "ERROR!!! Duracion inválida para el evento");


    if (clasificacion == "+13" && (hora < 6 || hora > 22))
        return (false, "ERROR!!! Horario no permitido para  +13");


    if (clasificacion == "+18" && !(hora >= 22|| hora <=5))
        return (false, "ERROR!!! Horario no permitido para +18");


    if (produccion == "bajo" &&  clasificacion == "+18")
        return (false, "ERROR!!! Producción no permitida para +18");

    return (true, "Contenido válido");
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

    Console.WriteLine();

    Console.WriteLine("#===== REGLAS DEL SISTEMA =====#");

    Console.WriteLine(" ===== Clasificación =====");
    Console.WriteLine("tp: cualquier hora");
    Console.WriteLine("+13: entre 6 y 22");
    Console.WriteLine("+18: entre 22 y 5");

    Console.WriteLine("                   ");

    Console.WriteLine("#=====  Duración por Tipo  =======#");
    Console.WriteLine("Película: 60-180 min");
    Console.WriteLine("Serie: 20-90 min");
    Console.WriteLine("Documental: 30-120 min");
    Console.WriteLine("Evento: 30-240 min");
    Console.WriteLine("                   ");
    Console.WriteLine("                   ");

}

void MostrarEstadisticas()
{
    Console.WriteLine("                         ");
    Console.WriteLine("#==== ESTADISTICAS =====#");

    Console.WriteLine("Total Evaluados: " + totalEvaluados);
    Console.WriteLine("Publicados: " + publicados);
    Console.WriteLine("Rechazados: " + rechazados);
    Console.WriteLine("En revisión: " + enRevision);

    Console.WriteLine("                           ");
    Console.WriteLine("                           ");

    string impactoPredominante = "Ninguno";

    if (totalEvaluados == 0)
    {
        impactoPredominante = "Ninguno";
    }

    else if (impactoAlto >= impactoMedio && impactoAlto >= impactoBajo)
    {
        impactoPredominante = "Alto";
    }
    else if (impactoMedio >=  impactoBajo)
    {
        impactoPredominante = "Medio";
    }
    else    
    {
        impactoPredominante = "Bajo";
    }

    Console.WriteLine("Impacto predominante: " + impactoPredominante);

    if (totalEvaluados > 0)
    {
        double porcentaje = (double)publicados / totalEvaluados * 100;
        Console.WriteLine($"Porcentaje de aprobación {porcentaje:F2}%");
    }

}

void ReiniciarEstadisticas()
{
    totalEvaluados = 0;
    publicados = 0;
    rechazados = 0;
    enRevision = 0;

    impactoAlto = 0;
    impactoMedio = 0;
    impactoBajo = 0;

    Console.WriteLine("Estadísticas reiniciadas correctamente!");
}



