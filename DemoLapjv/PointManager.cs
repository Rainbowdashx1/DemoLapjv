using System.Windows;

namespace DemoLapjv
{
    public class PointManager
    {
        private Random rand;
        public PointManager()
        {
            rand = new Random();
        }
        public List<Point> CreateFigure(int NumPoints, double canvasWidth, double canvasHeight)
        {
            var destinationPoints = new List<Point>();
            bool useCircle = rand.NextDouble() < 0.5; // 50% de probabilidad
            if (useCircle)
            {
                //Circulo
                Point center = new Point(canvasWidth / 2, canvasHeight / 2);
                double radius = Math.Min(canvasWidth, canvasHeight) / 4;
                for (int i = 0; i < NumPoints; i++)
                {
                    double angle = 2 * Math.PI * i / NumPoints;
                    double x = center.X + radius * Math.Cos(angle);
                    double y = center.Y + radius * Math.Sin(angle);
                    destinationPoints.Add(new Point(x, y));
                }
            }
            else
            {
                // Cuadrado
                double side = Math.Min(canvasWidth, canvasHeight) / 2;
                double left = (canvasWidth - side) / 2;
                double top = (canvasHeight - side) / 2;
                double perimeter = 4 * side;
                for (int i = 0; i < NumPoints; i++)
                {
                    double pos = perimeter * i / NumPoints;
                    double x, y;
                    if (pos < side)
                    {
                        x = left + pos;
                        y = top;
                    }
                    else if (pos < 2 * side)
                    {
                        x = left + side;
                        y = top + (pos - side);
                    }
                    else if (pos < 3 * side)
                    {
                        x = left + side - (pos - 2 * side);
                        y = top + side;
                    }
                    else
                    {
                        x = left;
                        y = top + side - (pos - 3 * side);
                    }
                    destinationPoints.Add(new Point(x, y));
                }
            }

            return destinationPoints;
        }
        public List<Point> CreatePoints(int NumPoints, double canvasWidth, double canvasHeight)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < NumPoints; i++)
            {
                double x = rand.NextDouble() * canvasWidth;
                double y = rand.NextDouble() * canvasHeight;
                points.Add(new Point(x, y));
            }
            return points;
        }
        //Distancia euclidiana 
        public double Distance(Point p1, Point p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
