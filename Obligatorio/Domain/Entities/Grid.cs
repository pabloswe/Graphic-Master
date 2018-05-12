﻿using Domain.Exceptions;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class Grid
    {
        public string GridName { get; set; }
        public Client Client { get; set; }
        public Designer Designer { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Wall> Walls { get; set; }
        public List<WallBeam> WallBeams { get; set; }
        public List<Opening> Openings { get; set; }
        public static int PixelConvertion = 25;
        public int maxMeters = 5;
        private Pen gridPen;

        public Grid() { }

        public Grid (string gridName, Client client, int height, int width)
        {
            this.Walls = new List<Wall>();
            this.WallBeams = new List<WallBeam>();
            this.Openings = new List<Opening>();

            this.GridName = gridName;
            this.Client = client ;
            this.Height = height * PixelConvertion; 
            this.Width = width * PixelConvertion;

            this.gridPen = new Pen(Color.Black, 2);
        }

        public void DrawGrid(Graphics graphic)
        {
            this.DrawX(graphic);
            this.DrawY(graphic);
        }

        private void DrawX(Graphics graphic)
        {
            for (int i = PixelConvertion; i < this.Height; i += PixelConvertion)
            {
                Point startPoint = new Point(0, i);
                Point endPoint = new Point(this.Width, i);

                graphic.DrawLine(gridPen, startPoint, endPoint);

            }
        }

        private void DrawY(Graphics graphic)
        {
            for (int i = PixelConvertion; i < this.Width; i += PixelConvertion)
            {

                Point startPoint = new Point(i, 0);
                Point endPoint = new Point(i, this.Height);

                graphic.DrawLine(gridPen, startPoint, endPoint);
            }
        }

        public bool validateWall(Wall wall)
        {
            return true;
        }

        public void AddWall(Graphics graphic, Wall wall)
        {
            this.IsValid(wall);
            if (wall.SizeGreaterThanMaximum()) //si el largo del wall es > 5 + refactor al nombre
            {
                Wall anotherWall = new Wall(wall.startUbicationPoint, wall.CalculateLocationPoint(maxMeters));//creo una nueva pared
                                                                                                              //le seteo el punto inicial igual a wall
                                                                                                              //le seteo el punto final en base a X,Y de wall 5 metros despues
                AddWall(graphic, anotherWall);//llamo recursivo con anotherWall 
                wall.startUbicationPoint = wall.CalculateLocationPoint(maxMeters); //corro el punto inicial de wall
                AddWall(graphic, wall);//llamo recursivo con la nueva wall   
            }
            if (CutAWall(wall))//verifico si alguna pared la corta
            {
                Point intersection = wall.FirstIntersection(wall); //obtengo la interseccion con la primera pared
                Wall newWall = new Wall(wall.startUbicationPoint, intersection); //Si la corta agrego creo una pared desde el punto inicial hasta donde se corta
                newWall.Draw(graphic);
                this.Walls.Add(newWall);
                AddWallBeam(graphic, newWall.startUbicationPoint);//verifico si tengo que insertar viga en inicio + cortar paredes
                AddWallBeam(graphic, newWall.endUbicationPoint);//verifico si tengo que insertar viga en final + cortar paredes
                wall.startUbicationPoint = intersection; // modifico el punto inicial de wall para que sea en donde se corta con la otra pared
                AddWall(graphic, wall);//llamo recursivo con wall
            }
            else
            {
                wall.Draw(graphic);
                this.Walls.Add(wall);
                AddWallBeam(graphic, wall.startUbicationPoint);//agrego viga inicial
                AddWallBeam(graphic, wall.endUbicationPoint);//agrego viga final
            }

        }

        private bool CutAWall(Wall wall)
        {
            bool isCutting = false;
            foreach (Wall anotherWall in this.Walls)
            {
                foreach(Point anotherPoint in anotherWall.Path)
                {
                    foreach(Point point in wall.Path)
                    {
                        if (point.Equals(anotherPoint)){
                            return true;
                        }
                    }
                }
            }
            return isCutting;
        }

        private void IsValid(Wall wall)
        {
            this.Exist(wall);
            this.ContainedIn(wall);
        }

        private void Exist(Wall wall)
        {
            if (this.Walls.Contains(wall))
            {
                throw new ExceptionController(ExceptionMessage.WALL_ALREADY_EXSIST);
            }
        }

        private void ContainedIn(Wall wall)
        {
            List<Point> intersectionPoints = new List<Point>();
            foreach(Wall anotherWall in this.Walls)
            {
                Point firstPoint = wall.FirstIntersection(anotherWall);
                if (!firstPoint.IsEmpty)
                {
                    Wall newWall = new Wall(firstPoint, wall.endUbicationPoint);
                    Point secondPoint = newWall.FirstIntersection(anotherWall);
                    if(!secondPoint.IsEmpty)
                        throw new ExceptionController(ExceptionMessage.USER_INVALID_PASSWORD); // cambiar exception
                }
            }
        }

        public void AddWallBeam(Graphics graphic, Point ubicationPoint)
        {
            if (ValidWallBeam(ubicationPoint))
            {
                WallBeam wallBeam = new WallBeam(ubicationPoint);
                wallBeam.Draw(graphic);
            };
        }

        private bool ValidWallBeam(Point ubicationPoint)
        {
            return (this.WallBeams.First(wallBeam => wallBeam.UbicationPoint.Equals(ubicationPoint))).Equals(0);
        }

        public void AddOpening(Opening opening)
        {

        }

        public void RemoveWall(Wall wall)
        {
            WallBeam startWallBeam = getWallBeam(wall.startUbicationPoint);
            WallBeam endWallBeam = getWallBeam(wall.endUbicationPoint);
            this.Walls.Remove(wall);
            RemoveWallBeam(startWallBeam);
            RemoveWallBeam(endWallBeam);
        }

        private WallBeam getWallBeam(Point startUbicationPoint)
        {
            return WallBeams.First(wallBeam => wallBeam.UbicationPoint.Equals(startUbicationPoint));
        }

        public void RemoveWallBeam(WallBeam wallBeam)
        {
            List<Wall> useAWallBeam = new List<Wall>();
            foreach(Wall wall in this.Walls)
            {
                if (ContainsPoint(wall.Path, wallBeam.UbicationPoint))
                {
                    useAWallBeam.Add(wall);
                }
            }
            if (useAWallBeam.Count.Equals(0))
            {
                this.WallBeams.Remove(wallBeam);
            }
        }

        private bool ContainsPoint(List<Point> list, Point point)
        {
            foreach (Point anotherPoint in list)
            {
                if (anotherPoint.Equals(point))
                    return true;
            }
            return false;
        }

        public void RemoveOpening(Wall wall)
        {

        }

        public override bool Equals(object gridObject)
        {
            bool isEqual = false;
            if (gridObject != null && this.GetType().Equals(gridObject.GetType()))
            {
                Grid grid = (Grid)gridObject;
                if ((this.Client.Equals(grid.Client)))
                {
                    isEqual = true;
                }

            }
            return isEqual;
        }

    }
}
