+	﻿using System;
+	using System.Collections.Generic;
+	using System.Linq;
+	using System.Text;
+	using System.Threading.Tasks;
+	
+	namespace ConsoleApplication4
+	{
+	
+	    // Класс "Работник в общем"
+	    public class Worker
+	    {
+	        public string Name;
+	        public string Job;
+	        public int Salary;
+	
+	        public Worker() { }
+	
+	        public override bool Equals(object obj)
+	        {
+	            if (obj.GetType() != this.GetType()) return false;
+	
+	            Worker other = (Worker)obj;
+	            return (this.Name == other.Name) && (this.Job == other.Job) && (this.Salary == other.Salary);
+	        }
+	
+	        public override int GetHashCode()
+	        {
+	            return Hash(Name) + Hash(Job) + Salary;
+	        }
+	
+	        public override String ToString()
+	        {
+	            return String.Format("{0} {1} {2}", this.Name, this.Job, this.Salary);
+	        }
+	
+	        private int Hash(string str)
+	        {
+	            int sum = 0;
+	            foreach (char ch in str)
+	                sum += ch;
+	            return sum;
+	        }
+	    }
+	
+	    // Класс "Бухгалтер" 
+	    public class Accountant : Worker
+	    {
+	        public Accountant(string Name, int Salary)
+	        {
+	            this.Name = Name;
+	            this.Job = "Бухгалтер";
+	            this.Salary = Salary;
+	        }
+	
+	        public Accountant() { }
+	
+	        public void ChangeSalary(Worker obj, int newSalary)// метод
+	        {
+	            obj.Salary = newSalary;
+	        }
+	    }
+	
+	    // Класс "Главный Бухгалтер"
+	    public class MainAccountant : Accountant
+	    {
+	        public MainAccountant(string Name, int Salary)
+	        {
+	            this.Name = Name;
+	            this.Job = "Главный бухгалтер";
+	            this.Salary = Salary;
+	        }
+	
+	        public void CalcSalary(Worker[] Salaryworker) // метод
+	        {
+	            int sum = 0;
+	            foreach (Worker MasRab in Salaryworker)
+	            {
+	                sum += MasRab.Salary;
+	            }
+	            Console.WriteLine("---");
+	            Console.WriteLine("Общая зарплата всех работников: {0} рублей", sum);
+	        }
+	    }
+	
+	    // Класс "Рабочий"
+	    public class Workers : Worker
+	    {
+	        public int planSalary;
+	        public int factSalary;
+	        public int time;
+	
+	        public Workers(string Name, int Salary)
+	        {
+	            this.Name = Name;
+	            this.Job = "Рабочий";
+	            this.Salary = Salary;
+	        }
+	
+	        public void PutData(int planSalary, int factSalary, int time)// метод
+	        {
+	            this.planSalary = planSalary;
+	            this.factSalary = factSalary;
+	            this.time = time;
+	        }
+	    }
+	
+	    // Класс "Инженер"
+	    public class Engineer : Worker
+	    {
+	        public Engineer(string Name, int Salary)
+	        {
+	            this.Name = Name;
+	            this.Job = "Инженер";
+	            this.Salary = Salary;
+	        }
+	
+	        public void Production(Workers Raboch) //метод
+	        {
+	            Console.WriteLine("---");
+	            Console.WriteLine("Производство - {0}: плановая зарплата - {1} рублей,  фактическая зарплата - {2} рублей, количество рабочих дней -  {3}", Raboch.Name, Raboch.planSalary, Raboch.factSalary, Raboch.time);
+	        }
+	    }
+	
+	    class Program
+	    {
+	        static void Main(string[] args)
+	        {
+	            Worker[] rabotniki = new Worker[4]; // Массив объектов класса "Работник в общем"
+	            rabotniki[0] = new Accountant("Иванов Иван Иванович", 20000 );
+	            rabotniki[1] = new MainAccountant("Петров Петр Петрович", 30000 );
+	            rabotniki[2] = new Workers("Сидорова Александра Игоревна", 40000);
+	            rabotniki[3] = new Engineer("Матросова Анастасия Александровна", 50000);
+	
+	            foreach (Worker MasRab in rabotniki)
+	            {
+	                Console.WriteLine("Работник: {0}", MasRab.ToString());
+	            }
+	
+	            Accountant Acc1 = (Accountant)rabotniki[0];
+	            Acc1.ChangeSalary(rabotniki[3], 55000);
+	            Console.WriteLine("---");
+	            Console.WriteLine("Новая зарплата для 4-его работника: \n{0} рублей", rabotniki[3].ToString());
+	
+	            MainAccountant MAcc1 = (MainAccountant)rabotniki[1];
+	            MAcc1.CalcSalary(rabotniki);
+	
+	            Workers Wor1 = (Workers)rabotniki[2];
+	            Wor1.PutData(40000, 45000, 270);
+	
+	            Engineer Eng1 = (Engineer)rabotniki[3];
+	            Eng1.Production((Workers)rabotniki[2]);
+	
+	            Console.WriteLine("\nДля выхода нажмите любую клавишу.");
+	            Console.ReadKey();
+	        }
+	    }
+	}
+	
+	    
+	
