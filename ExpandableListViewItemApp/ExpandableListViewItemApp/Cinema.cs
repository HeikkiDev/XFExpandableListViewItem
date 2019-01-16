using System;
using System.Collections.Generic;
using System.Text;

namespace ExpandableListViewItemApp
{
    public class Cinema
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public double ViewPointY { get; set; }
        public double gridHeight { get; set; }
        public double gridWidth { get; set; }

        public static List<Cinema> GetFakeCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>();

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cine Albéniz",
                    Adress = "Alcazabilla, 4",
                    Phone = "951 57 11 15"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Golem",
                    Adress = "Calle de Martín de los Heros, 14",
                    Phone = "915 59 38 36"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Renoir Plaza de España",
                    Adress = "Calle de Martín de los Heros, 12",
                    Phone = "915 42 27 02"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cine Capitol",
                    Adress = "Calle Gran Vía, 41",
                    Phone = "915 22 22 29"
                }
            );

            cinemas.Add(
                new Cinema()
                {
                    Name = "Cines Callao",
                    Adress = "Plaza Callao, 3",
                    Phone = "915 22 58 01"
                }
            );

            return cinemas;
        }
    }
}
