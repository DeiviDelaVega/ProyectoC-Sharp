using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

public static class CloudinaryConfig
{
    public static Cloudinary GetInstance()
    {
        Account account = new Account(
            "franksito", //nombre de la nube
            "178427295139853", //codigo api key
            "xp1PweiX8TERNyf9vIawDmG-qDg" // codigo api key secret
        );

        Cloudinary cloudinary = new Cloudinary(account);
        cloudinary.Api.Secure = true;

        return cloudinary;
    }
}
