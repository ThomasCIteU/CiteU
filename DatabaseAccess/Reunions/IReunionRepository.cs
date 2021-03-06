﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Reunion
{
    public interface IReunionRepository
    {
        List<ReunionModel> GetAllReunions();
        List<ReunionModel> GetAllReunions(int idPole);
        void CreateReunion(DateTime date, int idResponsable, string Lieu, int idPole);
        void DeleteReunion(int IdReunion);
        ReunionModel GetReunion(int idReunion);
        void EditReunion(int idReunion, DateTime date, int idResponsable, string lieu, int idPole);
        ReunionModel GetReunion(DateTime date);
        ReunionModel GetReunion(DateTime date, int idPole);
    }
}
