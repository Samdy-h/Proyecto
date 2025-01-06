using System;
using System.Collections.Generic;
using Spectre.Console;
    enum Casilla
    {
        Walls,//representa un muro
        trampa1,//representa un tipo de trampa
        trampa2,//representa 
        trampa3,
        Free
    }

class Maze
{
    static void Main()
    {
        int ancho=33;
        int altura=33;
        Casilla[,] laberinto= GetMaze(ancho, altura);
        PrintMaze(laberinto);
    }
    static  Casilla[,] GetMaze(int ancho, int altura)
    {
        Casilla[,] laberinto= new Casilla[ancho, altura];
        Random rnd= new Random();

        for (int i=0;i<ancho;i++)
        {
            for(int j=0;j<altura;j++)
            {
                laberinto[i, j] = Casilla.Walls;
            }
        }
        CrearCamnos(laberinto,1,1);
        laberinto[0,1]=Casilla.Free;
        laberinto[altura-1,ancho-2]=Casilla.Free;

        AgregarTrampas(laberinto,3);

        return laberinto;
    }
    static void  CrearCamnos(Casilla[,] laberinto, int x, int y)
    {
        var directions= new List<(int dx,int dy)>
        {
            (0,-2),
            (0,2),
            (-2,0),
            (2,0)
        };
        var rnd= new Random();
        directions.Sort((a,b) =>rnd.Next().CompareTo(rnd.Next()));
        foreach (var (dx,dy) in directions)
        {
            int nx=x+dx;
            int ny=y+dy;
            if(nx>0&&nx<laberinto.GetLength(1)&&ny>0&&ny<laberinto.GetLength(0)&&laberinto[ny,nx]==Casilla.Walls)
            {
                laberinto[ny,nx]=Casilla.Free;
                laberinto[y+dy/2,x+dx/2]=Casilla.Free;
                CrearCamnos(laberinto,nx,ny);
            }
        }
    }
    static void AgregarTrampas(Casilla[,] laberinto,int cantidadTrampas)
    {
        var rnd=new Random();
        int filas=laberinto.GetLength(0);
        int columnas=laberinto.GetLength(1);

        for (int i=0;i<cantidadTrampas;i++)
        {
            while(true)
            {
                int x=rnd.Next(1,columnas-1);//evitar bordes
                int y=rnd.Next(1,filas-1);//evitar bordes
                if(laberinto[y,x]==Casilla.Free)//solo en casillas libres
                {
                    switch (i)
                    {
                        case 0:
                        laberinto[y,x]=Casilla.trampa1;
                        break;
                        case 1:
                        laberinto[y,x]=Casilla.trampa2;
                        break;
                        case 2:
                        laberinto[y,x]=Casilla.trampa3;
                        break;
                    }
                    break;//salir del bucle de colocar trampas
                }
            }
        }
    }
    static void PrintMaze(Casilla[,] laberinto)
    {
        int filas=laberinto.GetLength(0);
        int columnas=laberinto.GetLength(1);
        for(int i=0;i<filas;i++)
        {
            for(int j=0;j<columnas;j++)
            {
                switch (laberinto[i,j])
                {
                    case Casilla.Walls:
                    AnsiConsole.Markup("[blue]&[/]");//paredes
                    break;
                    case Casilla.Free:
                    AnsiConsole.Markup("[white]#[/]");//camino
                    break;
                    case Casilla.trampa1:
                    AnsiConsole.Markup("[yellow]![/]");
                    break;
                    case Casilla.trampa2:
                    AnsiConsole.Markup("[red]![/]");
                    break;
                    case Casilla.trampa3:
                    AnsiConsole.Markup("[magenta]![/]");
                    break;
                }
            }
            Console.WriteLine();
        }
    }
}