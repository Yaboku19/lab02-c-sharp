using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private readonly IDictionary<String, ISet<TUser>> _followedUsers = new Dictionary<String, ISet<TUser>>(); 
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (!_followedUsers.ContainsKey(group))
            {
                _followedUsers[group] = new HashSet<TUser>();
                _followedUsers[group].Add(user);
            }
            return _followedUsers[group].Add(user);
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var allFriends = new HashSet<TUser>();
                foreach (String group in _followedUsers.Keys)
                {
                    foreach (TUser friend in _followedUsers[group])
                    {
                        allFriends.Add(friend);
                    }
                }
                return new List<TUser>(allFriends);
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (_followedUsers.ContainsKey(group))
            {
                return new HashSet<TUser>(_followedUsers[group]);
            }
            else
            {
                return new HashSet<TUser>();
            }
        }
    }
}
