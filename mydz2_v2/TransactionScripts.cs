﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace mydz2_v2
{
    public struct Error
    {
        public bool error;
        public string message;
    }
    public static class TransactionScripts
    {

        public static Error createUser(string login, string fname, string lname, string email, string password)
        {
            Error res;
            DataTable dt = Users.findByValue("ulogin", $"'{login}'");
            if (dt.Rows.Count == 0)
            {
                Users us = new Users();
                us.ulogin = login;
                us.fname = fname;
                us.lname = lname;
                us.email = email;
                string hash = encryption.HashPassword(password);
                us.upassword = hash;
                //string debug = us.getQuery();
                us.save();

                res.error = false;
                res.message = "Даныне добавлены в БД";
            }
            else
            {
                res.error = true;
                res.message = "Данный логин уже существует";
            }

            return res;
        }
        public static Error auth(string ulogin, string upassword)
        {
            Error result;
            result.error = true;

            Users us = new Users(ulogin);
            if (us.isInDB())
            {
                if (encryption.VerifyHashedPassword(us.upassword, upassword))
                {
                    result.error = false;
                    result.message = "Авторизация прошла успешно";
                }
                else
                    result.message = "Пароль не совпал";
            }
            else
                result.message = "Нет такого пользователя";

            return result;
        }
        public static int getUserId(string ulogin) {
            Users user = new Users(ulogin);
            return user.id;
        }
        public static DataTable getEventById(int id)
        {
            if (id <= 0)
            {
                return new DataTable();
            }

            Event ev = new Event(id);
            if (ev.isInDB()) 
            { 
                return ev.asDataTable();
            }
            else
            {
                return new DataTable();
            }
        }
        public static DataTable getAllEvents()
        {
            return Event.showAll();
        }
        public static DataTable getAllUsers()
        {
            return Users.showAll();
        }
        public static Error createEvent(string eventType, string place, 
                string date, string time) 
        {
            Error res;
            DateTime dtime;
            Event ev = new Event();
            try
            {
                DateTime.TryParse(
                        $"{date} {time}",
                        System.Globalization.CultureInfo.CurrentCulture,
                        System.Globalization.DateTimeStyles.AssumeLocal,
                        out dtime
                    );

                ev.eventType = eventType;
                ev.place = place;
                ev.date = dtime;

                ev.save();

                res.error = false;
                res.message = "Событие успешно добавлено";
            }
            catch (Exception e)
            {
                res.error = true;
                res.message = e.ToString();
            }
            return res;

        }
        public static Error deleteEvent(int event_id)
        {
            Error er;

            Event ev = new Event(event_id);
            if (ev.isInDB())
            {
                ev.delete();
                er.error = false;
                er.message = "Ok";
            }
            else
            {
                er.error = true;
                er.message = "Данного события не существует";
            }
            return er;
        }
        public static Error updateEvent(Event ev, string eventType, string place, string date, string time)
        {
            DateTime dtime;
            DateTime.TryParse(
                $"{date} {time}",
                System.Globalization.CultureInfo.CurrentCulture,
                System.Globalization.DateTimeStyles.AssumeLocal,
                out dtime);

        Error result;
            result.error = false;
            string update_fields = "";

            if (ev.eventType != eventType) { 
                ev.eventType = eventType;
                update_fields = update_fields + "Тип события  ";
            }
            if (ev.place != place)
            {
                ev.place = place;
                update_fields = update_fields + "Место  ";
            }
           
            
            if (ev.date.ToString("yyyy-MM-dd") != dtime.ToString("yyyy-MM-dd"))
            {
                update_fields = update_fields + "Дата  ";
            }
            if (ev.date.ToString("HH-mm") != dtime.ToString("HH-mm"))
            {
                update_fields = update_fields + "Время  ";
                ev.date = dtime;
            }
            

            try
            {
                if(update_fields != "") 
                { 
                    ev.save();
                    result.message = $"Поля обновлены: {update_fields}";
                }
                else
                {
                    result.message = "Нечего обновлять";
                }
                result.error = false;
                return result;
            }
            catch(Exception e)
            {
                result.error = true;
                result.message = e.ToString();
                return result;
            }
        }
    }
}
