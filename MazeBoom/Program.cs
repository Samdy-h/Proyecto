using System;
using System.Collections.Generic;
using Spectre.Console;
class Maze
{
    static void Main()
    {
        int ancho=33;
        int altura=33;
        Casilla[,] laberinto= GetMaze(ancho, altura);
        PrintMaze(laberinto);
    }
    enum Casilla
    {
        Walls,
        Free
    }
    static  Casilla[,] GetMaze(int ancho, int altura)
    {
        Casilla[,] laberinto= new Casilla[ancho, altura];
        for (int i=0;i<ancho;i++)
        {
            for(int j=0;j<altura;j++)
            {
                laberinto[i, j] = Casilla.Walls;
            }
        }
        CrearCamnos(laberinto,1,1);
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
    static void PrintMaze(Casilla[,] laberinto)
    {
        int filas=laberinto.GetLength(0);
        int columnas=laberinto.GetLength(1);
        for(int i=0;i<filas;i++)
        {
            for(int j=0;j<columnas;j++)
            {
                if(laberinto[i,j]==Casilla.Walls)
                {
                    AnsiConsole.Markup("[red]#[/]");
                }
                else
                {
                    AnsiConsole.Markup("[green][/]");
                }
            }
            Console.WriteLine();
        }
    }
}