namespace BinhDinhFood.Presentation.Models.Report;

public class Bar
{
    public string type = "bar";

    public Data data;
    public Options options = new Options();


    public class Options
    {
        public Plugins plugins = new Plugins();
        public Scales scales = new Scales();
        public class Plugins
        {
            public Title title = new Title();
            public class Title
            {
                public bool display = true;
                public string text;
            }
        }
        public class Scales
        {
            public Y y = new Y();
            public class Y
            {
                public bool beginAtZero = true;
            }
        }
    }
    public class Data
    {
        public string[] labels;
        public Dataset[] datasets = new Dataset[1];
        public class Dataset
        {
            public string label;
            public string[] backgroundColor;// = new string[2]
            public string[] borderColor;
            public int borderWidth = 1;
            public float[] data;
            public Dataset(int soLuong)
            {
                backgroundColor = new string[soLuong];
                borderColor = new string[soLuong];
                data = new float[soLuong];
            }
        }
        public Data(int soLuong)
        {
            if (soLuong > 0)
            {
                labels = new string[soLuong];
                datasets = new Dataset[1];
                datasets[0] = new Dataset(soLuong);
            }
        }
    }
    public Bar(int soLuong)
    {
        data = new Data(soLuong);
    }
}
