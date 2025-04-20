namespace BinhDinhFood.Presentation.Models;

public class Pie
{
    public string type = "pie";

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
        public Dataset[] datasets = new Dataset[1];
        public class Dataset
        {
            public string[] backgroundColor;// = new string[2]
            public float[] data;
            public Dataset(int sogoi)
            {
                backgroundColor = new string[sogoi];
                data = new float[sogoi];
            }
        }
        public Data(int sogoi)
        {
            labels = new string[sogoi];
            datasets = new Dataset[1];
            datasets[0] = new Dataset(sogoi);
        }
    }
    public Pie(int sogoi)
    {
        data = new Data(sogoi);
    }
}
