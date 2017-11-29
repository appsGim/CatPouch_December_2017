using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HapoalimClientSideAPI.Models
{
    public class UserVoteInputClass : BaseClass
    {
        public int UserID { get; set; }
        public int BuissinesID { get; set; }
        public int VoteType { get; set; }
        public int AddressCombinationID { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public Guid APITransaction { get; set; }
    }

    public class CityInputClass : BaseClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class NeighbourhoodInputClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class StreetsInputClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class AddressCombinationClass
    {
        public int ID { get; set; }
        public int CityID { get; set; }
        public int StreetID { get; set; }
        public int NeighbourhoodID { get; set; }
    }

    public class BussinesesInputClass
    {
        public int ID { get; set; }
        public string BussinesName { get; set; }
        public int CategoryID { get; set; }
    }

    public class BussinessCateogriesInputClass
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }


    public class BaseClass
    {
        public string ClientToServerGuid { get; set; }//knows only to us
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string MAC { get; set; }
        public string OS { get; set; }

    }

    public enum enumVoteType { Vote = 1, Proposal = 2 }

    public class UserResult
    {
        private System.Nullable<System.Guid> _UniqueToken;
        private int _SubmittedTimes;
        private int _VotesCount;
        private int _ProposalCount;

        public System.Nullable<System.Guid> UniqueToken
        {
            get
            {
                return this._UniqueToken;
            }
            set
            {
                if ((this._UniqueToken != value))
                {
                    this._UniqueToken = value;
                }
            }
        }

        public int SubmittedTimes
        {
            get
            {
                return this._SubmittedTimes;
            }
            set
            {
                if ((this._SubmittedTimes != value))
                {
                    this._SubmittedTimes = value;
                }
            }
        }

        public int VotesCount
        {
            get
            {
                return this._VotesCount;
            }
            set
            {
                if ((this._VotesCount != value))
                {
                    this._VotesCount = value;
                }
            }
        }

        public int ProposalCount
        {
            get
            {
                return this._ProposalCount;
            }
            set
            {
                if ((this._ProposalCount != value))
                {
                    this._ProposalCount = value;
                }
            }
        }
    }
}