/*
 * Copyright 2009 Daniel Hölbling - http://www.tigraine.at
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace ImagineClub.Web
{
    using System;
    using System.Reflection;
    using Castle.MonoRail.Framework;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]  
    public class DefaultValueAttribute : Attribute, IParameterBinder  
    {  
        private readonly object value;  
        public DefaultValueAttribute(object value)  
        {  
            this.value = value;  
        }  
  
        public int CalculateParamPoints(IEngineContext context, IController controller, IControllerContext controllerContext, ParameterInfo parameterInfo)  
        {  
            var token = context.Request[parameterInfo.Name];  
            if (CanConvert(parameterInfo.ParameterType, token))  
                return 10;  
            return 0;  
        }  
  
        private static bool CanConvert(Type targetType, string token)  
        {  
            if (token == null)  
                return false;  
  
            try  
            {  
                Convert.ChangeType(token, targetType);  
                return true;  
            }  
            catch (FormatException)  
            {  
                return false;  
            }  
        }  
  
        public object Bind(IEngineContext context, IController controller, IControllerContext controllerContext, ParameterInfo parameterInfo)  
        {  
            string token = context.Request[parameterInfo.Name];  
            Type type = parameterInfo.ParameterType;  
            if (CanConvert(type, token))  
                return Convert.ChangeType(token, type);  
            return value;  
        }  
    }
}