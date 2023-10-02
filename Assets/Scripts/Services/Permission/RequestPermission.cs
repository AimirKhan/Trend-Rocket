using UnityEngine;

namespace Services.Permission
{
    public class RequestPermission : MonoBehaviour
    {
        
        public void RequestStorageRead()
        {
            if (UnityEngine.Android.Permission.HasUserAuthorizedPermission(
                UnityEngine.Android.Permission.ExternalStorageRead))
            {
                Debug.Log("Permission has been granted.");
            }
            else
            {
                Debug.Log("Requesting permission");
                UnityEngine.Android.Permission.RequestUserPermission(
                    UnityEngine.Android.Permission.ExternalStorageRead);
                UnityEngine.Android.Permission.RequestUserPermission(
                    UnityEngine.Android.Permission.ExternalStorageWrite);
            }
        }
    }
}