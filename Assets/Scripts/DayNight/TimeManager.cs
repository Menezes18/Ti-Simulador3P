using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Date & Time Settings")]
    [Range(1, 28)]
    public int dateInMonth;
    [Range(1, 4)]
    public int season;
    [Range(1, 99)]
    public int year;
    [Range(0, 24)]
    public int hour;
    [Range(0, 6)]
    public int minutes;

    private DateTime DateTime;
    
    [Header("Tick Settings")]
    public int TickMinutesIncreased = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake(){
        DateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);

        //Debug.Log($"Starting Date: {DateTime.StartingDate(2)}");
        Debug.Log($"Start of a season: {DateTime.StartOfSeason(1, 3)}");
        Debug.Log($"Start of winter: {DateTime.StartOfWinter(3)}");
    }

    private void Start(){
        OnDateTimeChanged?.Invoke(DateTime);
    }

    private void Update(){
        currentTimeBetweenTicks += Time.deltaTime;

        if(currentTimeBetweenTicks >= TimeBetweenTicks){
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick(){
        AdvanceTime();
    }

    void AdvanceTime(){
        DateTime.AdvanceMinutes(TickMinutesIncreased);
        OnDateTimeChanged?.Invoke(DateTime);
    }
}

    [System.Serializable]

    public struct DateTime{
        #region Fields
        private Days day;
        private int date;
        private int year;

        private int hour;
        private int minutes;

        private Season season;

        private int totalNumDays;
        private int totalNumWeeks;
        #endregion
        #region Properties
        public Days Day => day;
        public int Date => date;
        public int Hour => hour;
        public int Minutes => minutes;
        public Season Season => season;
        public int Year => year;
        public int TotalNumDays => totalNumDays;
        public int TotalNumWeeks => totalNumWeeks;
        public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;
        #endregion
        #region Constructors
        public DateTime(int date, int season, int year, int hour, int minutes){
            this.day = (Days)(date % 7);
            if(day == 0) day = (Days)7;
            this.date = date;
            this.season = (Season)season;
            this.year = year;

            this.hour = hour;
            this.minutes = minutes;

            totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

            totalNumWeeks = 1 +totalNumDays / 7;
        }
        #endregion
        #region Time Advancement
        public void AdvanceMinutes(int SecondsToAdvanceBy){
            if(minutes + SecondsToAdvanceBy >= 60){
                minutes = (minutes + SecondsToAdvanceBy) % 60;
                AdvanceHour();
            }
            else{
                minutes += SecondsToAdvanceBy;
            }
        }
        private void AdvanceHour(){
            if((hour + 1) == 24){
                hour = 0;
                AdvanceDay();
            }
            else{
                hour++;
            }
        }
        private void AdvanceDay(){
            day++;

            if(day > (Days)7){
                day = (Days)1;
                totalNumWeeks++;
            }

            date++;

            if(date % 29 == 0){
                AdvanceSeason();
                date = 1;
            }

            totalNumDays++;
        }
        private void AdvanceSeason(){
            if(Season == Season.Inverno){
                season = Season.Primavera;
                AdvanceYear();
            }
            else season++;
        }
        private void AdvanceYear(){
            date = 1;
            year++;
        }
        #endregion
        #region Bool Checks
        public bool IsNight(){
            return hour > 18 || hour < 6;
        }
        public bool IsMorning(){
            return hour >= 6 && hour <= 12;
        }
        public bool IsAfternoon(){
            return hour > 12 && hour < 18;
        }
        public bool IsParticularDay(Days _day){
            return day == _day;
        }
        #endregion
        #region Key Dates
        public DateTime NewYearsDay(int year){
            if(year == 0) year = 1;
            return new DateTime(1, 0, year, 6, 0);
        }
        #endregion
        #region Start of Season
        public DateTime StartOfSeason(int season, int year){
            return StartOfSeason(season, year);
        }
        public DateTime StartOfSpring(int year){
            return StartOfSeason(0, year);
        }
        public DateTime StartOfSummer(int year){
            return StartOfSeason(1, year);
        }
        public DateTime StartOfAutumn(int year){
            return StartOfSeason(2, year);
        }
        public DateTime StartOfWinter(int year){
            return StartOfSeason(3, year);
        }
        #endregion
        #region To Strings
        public override string ToString(){
            return $"Data: {DateToString()} Estação: {season.ToString()} Tempo: {TimeToString()} " + $"\nDias totais: {totalNumDays} | Semanas Totais: {totalNumWeeks}";
        }
        public string DateToString(){
            return $"{Day} {Date} {Year.ToString("D2")}";
        }
        public string TimeToString(){
            return $"{hour.ToString("D2")}:{minutes.ToString("D2")}";
        }
        #endregion
    }

    [System.Serializable]
    public enum Days{
        NULL = 0,
        Dom = 1,
        Seg = 2,
        Ter = 3,
        Qua = 4,
        Qui = 5,
        Sex = 6,
        Sab = 7
    }
    [System.Serializable]
    public enum Season{
        Primavera = 0,
        Verao = 1,
        Outono = 2,
        Inverno = 3
    }