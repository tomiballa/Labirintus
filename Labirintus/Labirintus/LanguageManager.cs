using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Labirintus
{
	public class LanguageManager
	{
		Dictionary<string, Dictionary<string, string>> languages;
		string lang_id;

        public LanguageManager(String lid)
		{
			lang_id = lid;
			languages = new Dictionary<string, Dictionary<string, string>>();

            var text = File.ReadAllText("languages.json");
			languages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
        }

		public string parseText(string id)
		{
			return languages[lang_id][id];
		}
	}
}

