using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace SpaceShooter
{
    public class LocalizationSelector : MonoBehaviour
    {
       bool _isActive = false;
        private void Start() {
            ChangeLocale(PlayerPrefs.GetInt("LocaleID"));
        }


       public void ChangeLocale(int localeID){
            if(_isActive == true) return;
            StartCoroutine(SetLocale(localeID));
       }

        private IEnumerator SetLocale(int localeID){
            _isActive = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            _isActive = false;
            PlayerPrefs.SetInt("LocaleID",localeID);
        }

    }
}
