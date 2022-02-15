using System;

namespace ClothingManager.UI.MVC.Models{
    public class ErrorViewModel{
        public string RequestId{ get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}