using System.Drawing;

namespace BinhDinhFood.Presentation.Models.Report;

public class Line
{

    public string type = "line";
    public Data data;
    public Options options = new Options();


    public class Options
    {
        public Plugins plugins = new Plugins();
        public class Plugins
        {
            public Title title = new Title();
            public class Title
            {
                public bool display = true;
                public string text = "hi";
            }
        }
    }
    public class Data
    {
        public string[] labels;
        public Dataset[] datasets;
        public class Dataset
        {
            public string label;
            public float[] data;
            public bool fill = false;
            public string borderColor;
            public float tension = 0.45f;
            private static readonly Random rand = new Random();
            public Dataset(int soLuong)
            {
                data = new float[soLuong];
            }
            private string GetRandomColour()
            {
                int x = (DateTime.Now.Millisecond + DateTime.Now.Second) % 256;
                int y = (DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Hour) % 256;
                int z = (DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute) % 256;
                return Color.FromArgb(x, y, z, (x + y + z) % 256).ToString();
            }

        }
        public Data(int soLuong)
        {
            //datasets = new Dataset[soLuong];
            //for(int i= 0; i < datasets.Length; i++)
            //    datasets[i] = new Dataset(soLuong);
            labels = new string[soLuong];
        }
    }
    public Line(int SoLuong)
    {
        data = new Data(SoLuong);
    }

}
