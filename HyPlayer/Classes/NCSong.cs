﻿using System.Collections.Generic;

namespace HyPlayer.Classes
{
    public struct NCSong
    {
        public string sid;
        public string songname;
        public List<NCArtist> artist;
        public NCAlbum Album;
    }

    public struct NCPlayList
    {
        public string plid;
        public string cover;
        public string name;
        public NCUser creater;
    }

    public struct NCUser
    {
        public string id;
        public string name;
        public string avatar;
        public string signature;
    }

    public struct NCArtist
    {
        public string id;
        public string name;
    }

    public struct NCAlbum
    {
        public string id;
        public string name;
        public string cover;
    }
}