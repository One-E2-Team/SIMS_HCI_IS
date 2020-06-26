// File:    IAppointmentRepository.cs
// Author:  fmaster
// Created: Saturday, May 30, 2020 10:34:03 PM
// Purpose: Definition of Interface IAppointmentRepository

using Model.Appointments;
using System;
using System.Collections.Generic;

namespace Repository.Schedule
{
   public interface IAppointmentRepository : Repository.IRepositoryCRUD<Appointment, uint>
   {
      List<Appointment> GetSpan();
   
   }
}