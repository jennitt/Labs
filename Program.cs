using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplication4
{

    // Класс "Работник в общем"
    public class Worker
    {
        public string Name;
        public string Job;
        public int Salary;

       public Worker () { }

    // переопределенный метод GetType()
    new public Type GetType()
    {
        if (this is MainAccountant) return typeof(MainAccountant);
        if (this is Accountant) return typeof(Accountant);
        if (this is Engineer) return typeof(Engineer);
        if (this is Workers) return typeof(Workers);
        return typeof(object);
    }

    // переопределенный метод Equals(Object objA, Object objB)
    new public static bool Equals(Object objA, Object objB)
    {
        if ((objA==null) || (objB==null)) return false;    // защищаем от падения из-за null
        if (objA.GetType() != objB.GetType()) return false;  // если не совпадают типы данных, то ложь
        Worker first = (Worker)objA;
        Worker second = (Worker)objB;
        return (first.GetHashCode() == second.GetHashCode());
       
    }

    // переопределенный метод Finalize()
    new public void Finalize()
    {
        GC.SuppressFinalize(this);
    }
// переопределенный метод MemberwiseClone()
    new public Object MemberwiseClone()
    {
        Worker obj = new Worker();
        if (this is Accountant) obj = new MainAccountant(this.Name, this.Salary);
        else if (this is Accountant) obj = new Accountant(this.Name, this.Salary);
             else if (this is Engineer) obj = new Engineer(this.Name, this.Salary);
                  else if (this is Workers) obj =new Workers(this.Name, this.Salary);
        return obj;
    }

    // переопределенный метод ReferenceEquals(Object objA, Object objB)
    new public static bool ReferenceEquals(Object objA, Object objB)
    {
        if ((objA == null) && (objB == null)) return true;
        return Equals(objA, objB);
    }

        // переопределенный метод Equals(object obj)
        public override bool Equals(object obj)
        {
            if (obj == null) return false;    // защищаем от падения из-за null
            if (obj.GetType() != this.GetType()) return false;  // если не совпадают типы данных, то ложь

            Worker other = (Worker)obj;
            return (this.Name == other.Name) && (this.Job == other.Job) && (this.Salary == other.Salary);
        }
        // переопределенный метод GetHashCode()
        public override int GetHashCode()
        {
            var data = Encoding.Unicode.GetBytes(Name + Job + Convert.ToString(Salary)); // переводим в байты ФИО, Должность и Зарплату
            var md5 = MD5.Create(); // создаем объект класса MD5
            var result = md5.ComputeHash(data); // считаем хэш-код для наших байтов
            return BitConverter.ToInt32(result, 0); // возвращаем int32
            
        }
        
   
        // переопределенный метод ToString()
        public override String ToString()
        {
            return String.Format("{0} {1} {2} {3} {4}", this.Name, this.Job, this.Salary,"\n",this.GetHashCode(), this.GetType());
        }
       
       
    }

    // Класс "Бухгалтер" 
    public class Accountant : Worker
    {
        public Accountant(string Name, int Salary)
        {
            this.Name = Name;
            this.Job = "Бухгалтер";
            this.Salary = Salary;

        }

        public Accountant() { }

        public void ChangeSalary(Worker obj, int newSalary)// метод
        {
            obj.Salary = newSalary;
        }
    }

    // Класс "Главный Бухгалтер"
    public class MainAccountant : Accountant
    {
        public MainAccountant(string Name, int Salary)
        {
            this.Name = Name;
            this.Job = "Главный бухгалтер";
            this.Salary = Salary;
        }

        public void CalcSalary(Worker[] Salaryworker) // метод
        {
            if ((Salaryworker.Length != 0) && (Salaryworker != null)) // защищаем от пустого входного массива
            {
                int sum = 0;
                foreach (Worker MasRab in Salaryworker)
                {
                    sum += MasRab.Salary;
                }
                Console.WriteLine("---");
                Console.WriteLine("Общая зарплата всех работников: {0} рублей", sum);
            }
        }
    }
    // Класс "Рабочий"
    public class Workers : Worker
    {
        public int planSalary;
        public int factSalary;
        public int time;

        public Workers(string Name, int Salary)
        {
            this.Name = Name;
            this.Job = "Рабочий";
            this.Salary = Salary;
        }

        public void PutData(int planSalary, int factSalary, int time)// метод
        {
            this.planSalary = planSalary;
            this.factSalary = factSalary;
            this.time = time;
        }
    }

    // Класс "Инженер"
    public class Engineer : Worker
    {
        public Engineer(string Name, int Salary)
        {
            this.Name = Name;
            this.Job = "Инженер";
            this.Salary = Salary;
        }

        public void Production(Workers Raboch) //метод
        {
            Console.WriteLine("---");
            Console.WriteLine("Производство - {0}: плановая зарплата - {1} рублей,  фактическая зарплата - {2} рублей, количество рабочих дней -  {3}", Raboch.Name, Raboch.planSalary, Raboch.factSalary, Raboch.time);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Worker[] workers = new Worker[4]; // Массив объектов класса "Работник в общем"
            workers[0] = new Accountant("Иванов Иван Иванович", 20000);
            workers[1] = new MainAccountant("Петров Петр Петрович", 30000);
            workers[2] = new Workers("Сидорова Александра Игоревна", 40000);
            workers[3] = new Engineer("Матросова Анастасия Александровна", 50000);

           

            foreach (Worker MasRab in workers)
            {
                Console.WriteLine("Работник: {0}", MasRab.ToString());
            }

            Accountant Acc1 = (Accountant)workers[0];
            Acc1.ChangeSalary(workers[3], 55000);
            Console.WriteLine("---");
            Console.WriteLine("Новая зарплата для 4-его работника: \n{0} ", workers[3].ToString());

            MainAccountant MAcc1 = (MainAccountant)workers[1];
            MAcc1.CalcSalary(workers);

            // проверка как работает переопределенный метод Equals()
            MainAccountant MAcc2 = null; // проверка, если объект сравнения = null
            MAcc1.Equals(MAcc2);  // программа не падает при таких данных

            // проверка статического метода Equals(Object objA, Object objB)
                 MainAccountant MAcc3 = MAcc1;
                 if (Equals(MAcc1, MAcc3)) // программа не падает при таких данных
                 {
                     Console.WriteLine("---");
                     Console.WriteLine("Они равны! (2)");
                     Console.WriteLine("---");
                 }

            Workers Wor1 = (Workers)workers[2];
            Wor1.PutData(40000, 45000, 270);

            Engineer Eng1 = (Engineer)workers[3];
            Eng1.Production((Workers)workers[2]);

            Console.WriteLine("\nДля выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}

    
