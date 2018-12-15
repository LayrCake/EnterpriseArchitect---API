/*
Copyright 2015 Google Inc

Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;

namespace MailClient.Models
{
    public static class MyRequestedScopes
    {
        public static string[] Scopes
        {
            get
            {
                return new[] {
                    "openid",
                    //"email",
                    CalendarService.Scope.Calendar,
                    GmailService.Scope.GmailCompose,
                    GmailService.Scope.GmailInsert,
                    GmailService.Scope.GmailLabels,
                    GmailService.Scope.GmailModify,
                    GmailService.Scope.GmailSend
                };
            }
        }
    }
}