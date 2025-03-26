using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DemoLapjv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PointManager _pointManager;
        private const int NumPoints = 200; // Número de puntos a generar
        private List<Point> originPoints;
        private List<Point> destinationPoints;
        public MainWindow()
        {
            _pointManager = new PointManager();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Process();
        }
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            AnimationCanvas.Children.Clear();
            Process();
        }
        public void Process()
        {
            double canvasWidth = AnimationCanvas.ActualWidth;
            double canvasHeight = AnimationCanvas.ActualHeight;

            // Genero puntos de forma aleatoria 
            originPoints = _pointManager.CreatePoints(NumPoints, canvasWidth, canvasHeight);

            // Genero la figura de destino 50% circulo 50% cuadrado
            destinationPoints = _pointManager.CreateFigure(NumPoints, canvasWidth, canvasHeight);

            // Calculo la distancia euclidiana para la matris de costos 
            double[,] costMatrix = new double[NumPoints, NumPoints];
            for (int i = 0; i < NumPoints; i++)
            {
                for (int j = 0; j < NumPoints; j++)
                {
                    costMatrix[i, j] = _pointManager.Distance(originPoints[i], destinationPoints[j]);
                }
            }

            Polyline shapeLine = new Polyline
            {
                Stroke = Brushes.Green,
                StrokeThickness = 2
            };
            foreach (var pt in destinationPoints)
                shapeLine.Points.Add(pt);
            shapeLine.Points.Add(destinationPoints[0]);
            AnimationCanvas.Children.Add(shapeLine);

            // Resuelvo usando Lapjv en donde 'X' me dira en que posicion debe ir cada uno de los puntos dispersos
            LapjvCSharp.Lapjv lap = new LapjvCSharp.Lapjv();
            int[] assignment = lap.lapjvCsharp(costMatrix).x;

            // Agrego datos al datagrid
            List<CostItem> costItems = new List<CostItem>();
            for (int i = 0; i < NumPoints; i++)
            {
                costItems.Add(new CostItem
                {
                    Origen = $"P{i} ({originPoints[i].X:F1}, {originPoints[i].Y:F1})",
                    Destino = $"P{assignment[i]} ({destinationPoints[assignment[i]].X:F1}, {destinationPoints[assignment[i]].Y:F1})",
                    Costo = costMatrix[i, assignment[i]]
                });
            }
            CostDataGrid.ItemsSource = costItems;

            // Animacion de movimiento
            for (int i = 0; i < NumPoints; i++)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(ellipse, originPoints[i].X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, originPoints[i].Y - ellipse.Height / 2);
                AnimationCanvas.Children.Add(ellipse);

                Point start = originPoints[i];
                Point end = destinationPoints[assignment[i]];

                DoubleAnimation animX = new DoubleAnimation
                {
                    From = start.X - ellipse.Width / 2,
                    To = end.X - ellipse.Width / 2,
                    Duration = TimeSpan.FromSeconds(6),
                    BeginTime = TimeSpan.FromSeconds(0.5)
                };
                DoubleAnimation animY = new DoubleAnimation
                {
                    From = start.Y - ellipse.Height / 2,
                    To = end.Y - ellipse.Height / 2,
                    Duration = TimeSpan.FromSeconds(6),
                    BeginTime = TimeSpan.FromSeconds(0.5)
                };

                ellipse.BeginAnimation(Canvas.LeftProperty, animX);
                ellipse.BeginAnimation(Canvas.TopProperty, animY);
            }
        }
    }
    public class CostItem
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public double Costo { get; set; }
    }
}