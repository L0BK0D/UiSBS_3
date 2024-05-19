

public static class Program
{

    static uint  Fact(uint k)
    {
        if (k < 2)
            return 1;
        return k * Fact(k - 1);
    }
    static double RaiseToPow(double x, double power)
    {
        double res;
        int i;
        res = 1.0;
        if (power == 0)
        {
            return 1;
        }
        else if (power == 1)
        {
            return x;
        }

        else
            for (i = 1; i <= power; i++)
            {
                res = res * x;
            }
        return (res);
    }

    
    public static double Expon_Law(double lambda)
    {
        Random random = new();
        double k = random.NextDouble();
        double result = Math.Round(-Math.Log(k)/lambda, 2);
        return result;
    }
    public static void Async_Graph(double lambda, int T)
    {
        double[] timer = new double[T];
        double[] sendedArray = new double[T];
        double delay = 0;
        uint sended = 0;
        int queue = 0;
        int count = 1;
        uint num = 0;
        timer[0] = 0;
        int last = 0;
        for (int i = 1; i<T; i++) 
        {
            timer[i] = timer[i-1] + Expon_Law(lambda);
            Console.Write(i); Console.Write("\t"); Console.WriteLine(timer[i]);
        }
        sendedArray[0] = timer[0] + 1;
        for (int i = 1;i<T; i++)
        {
            if (timer[i] >= sendedArray[i-1]) 
            {
                sendedArray[i] = timer[i] + 1;
                if (sendedArray[i]<T)
                {
                    delay++;
                    sended++;
                }
            }
            else
            {
                sendedArray[i] = sendedArray[i - 1] + 1;
                if (sendedArray[i] < T)
                {
                    delay += sendedArray[i] - timer[i] ;
                    sended++;
                }   
            }
        }
        for (int i = 1; i<T; i++)
        {
            int fl = Convert.ToInt32(Math.Truncate(timer[i]));
            //if (fl - Convert.ToInt16(Math.Truncate(timer[i - 1])) > 1)
              //  queue = fl - Convert.ToInt16(Math.Truncate(timer[i - 1]));
            if (timer[i] < T && fl != last)
            {
                for (int j = i+1 ; j<T; j++)
                {
                    if (fl == Convert.ToInt16(Math.Truncate( timer[j])))
                    { count++; last = fl; }
                    else break;
                }
                queue += count;
                count = 1;
            }
            
        }
        Console.WriteLine($"Общее время задержки {delay}");
        Console.WriteLine($"Количество отравленных сообщений {sended}");
        Console.Write("Расчетное время средней задержки\t "); Console.WriteLine(delay / sended);
        Console.Write("Теоретическое время средней задержки \t"); Console.WriteLine((2-lambda)/(2*(1-lambda)));
        Console.Write("Расчетное среднее число заявок\t\t "); Console.WriteLine(Convert.ToDouble(queue) / T);
        Console.Write("Теоретическое среднее число заявок \t"); Console.WriteLine(lambda * (2 - lambda) / (2 * (1 - lambda)));

    }
    public static void Sync_Graph(double lambda, int T)
    {
        double[] timer = new double[T];
        double[] sendedArray = new double[T];
        double delay = 0;
        uint sended = 0;
        uint queue = 0;
        double message = 0;
        uint count = 0;
        uint index = 1;
        timer[0] = 0;
        for (int i = 1; i < T; i++)
        {
            timer[i] = timer[i - 1] + Expon_Law(lambda);
            //Console.WriteLine(timer[i]);
        }
        sendedArray[0] = timer[0] + 1;
        for (int i = 1; i < T; i++)
        {            
            if (Math.Truncate(timer[index]) < i)
            {
                sendedArray[index] = i+1;
                Console.WriteLine($"{i}\t{timer[index]}");
                if (sendedArray[i] < T)
                {
                    delay += sendedArray[index] - timer[index];
                    sended++;
                }
                index++;
            }
            else if (Math.Truncate(timer[index]) >= i)
            {
                Console.WriteLine($"{i}\t-");
            }
        }
        index = 1;
        for (int i = 0; i < T; i++)
        {
            for (uint j = index; j<T; j++)
            {
                if (Math.Truncate(timer[j]) == i)
                    count++;
                else if (Math.Truncate(timer[j]) > i) break;
            }
            queue += count;
            if (count > 0)
                index += count;
            //else index++;
            count = 0;
        }
        Console.WriteLine($"Общее время задержки {delay}");
        Console.WriteLine($"Количество отравленных сообщений {sended}");
        Console.Write("Расчетное время средней задержки\t "); Console.WriteLine(delay / sended);
        Console.Write("Теоритическое время средней задержки \t"); Console.WriteLine((2 - lambda) / (2 * (1 - lambda)) + 0.5);
        Console.Write("Расчетное среднее число заявок\t\t "); Console.WriteLine(Convert.ToDouble(queue) / T);
        Console.Write("Теоретическое среднее число заявок \t"); Console.WriteLine(lambda * (2 - lambda) / (2 * (1 - lambda)));
    }
    static void Main(string[] args)
    {
        Async_Graph(0.2, 10000);
    }

}