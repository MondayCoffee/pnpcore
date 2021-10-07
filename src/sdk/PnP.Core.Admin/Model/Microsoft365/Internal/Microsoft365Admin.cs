﻿using PnP.Core.Admin.Model.SharePoint;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.Core.Admin.Model.Microsoft365
{
    internal class Microsoft365Admin : IMicrosoft365Admin
    {
        private readonly PnPContext context;

        internal Microsoft365Admin(PnPContext pnpContext)
        {
            context = pnpContext;
        }

        #region Multi-geo support
        public async Task<bool> IsMultiGeoTenantAsync()
        {
            var result = await (context.Web as Web).RawRequestAsync(new ApiCall("sites?filter=siteCollection/root%20ne%20null&select=webUrl,siteCollection", ApiType.Graph), HttpMethod.Get).ConfigureAwait(false);

            #region Json responses
            /* Response if not multi-geo

            {
                "@odata.context": "https://graph.microsoft.com/v1.0/$metadata#sites",
                "value": [
                    {
                        "webUrl": "https://bertonline.sharepoint.com/",
                        "siteCollection": {
                            "hostname": "bertonline.sharepoint.com",
                            "root": {}
                        }
                    }
                ]
            }
 
            response if multi geo

            {
                "@odata.context": "https://graph.microsoft.com/beta/$metadata#sites",
                "value": [
                    {
                        "webUrl": "https://contoso.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"NAM",
                            "hostname": "contoso.sharepoint.com"
                        }
                    },
                    {
                        "webUrl": "https://contosoeur.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"EUR",
                            "hostname": "contosoeur.sharepoint.com"
                        }
                    },
                    {
                        "webUrl": "https://contosoapc.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"APC",
                            "hostname": "contosoapc.sharepoint.com"
                        }
                    }
                ]
            }
            */
            #endregion

            var root = JsonDocument.Parse(result.Json).RootElement.GetProperty("value");

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var siteInformation in root.EnumerateArray())
                {
                    if (siteInformation.TryGetProperty("siteCollection", out JsonElement siteCollection))
                    {
                        if (siteCollection.TryGetProperty("dataLocationCode", out JsonElement dataLocationCode))
                        {
                            if (dataLocationCode.ValueKind == JsonValueKind.String && !string.IsNullOrEmpty(dataLocationCode.GetString()))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool IsMultiGeoTenant()
        {
            return IsMultiGeoTenantAsync().GetAwaiter().GetResult();
        }

        public async Task<List<IGeoLocationInformation>> GetMultiGeoLocationsAsync()
        {
            var result = await(context.Web as Web).RawRequestAsync(new ApiCall("sites?filter=siteCollection/root%20ne%20null&select=webUrl,siteCollection", ApiType.Graph), HttpMethod.Get).ConfigureAwait(false);

            #region Json responses
            /* Response if not multi-geo

            {
                "@odata.context": "https://graph.microsoft.com/v1.0/$metadata#sites",
                "value": [
                    {
                        "webUrl": "https://bertonline.sharepoint.com/",
                        "siteCollection": {
                            "hostname": "bertonline.sharepoint.com",
                            "root": {}
                        }
                    }
                ]
            }
 
            response if multi geo

            {
                "@odata.context": "https://graph.microsoft.com/beta/$metadata#sites",
                "value": [
                    {
                        "webUrl": "https://contoso.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"NAM",
                            "hostname": "contoso.sharepoint.com"
                        }
                    },
                    {
                        "webUrl": "https://contosoeur.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"EUR",
                            "hostname": "contosoeur.sharepoint.com"
                        }
                    },
                    {
                        "webUrl": "https://contosoapc.sharepoint.com/",
                        "siteCollection": {
                            "dataLocationCode":"APC",
                            "hostname": "contosoapc.sharepoint.com"
                        }
                    }
                ]
            }
            */
            #endregion

            var root = JsonDocument.Parse(result.Json).RootElement.GetProperty("value");

            List<IGeoLocationInformation> dataLocations = null;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var siteInformation in root.EnumerateArray())
                {
                    if (siteInformation.TryGetProperty("siteCollection", out JsonElement siteCollection))
                    {
                        if (siteCollection.TryGetProperty("dataLocationCode", out JsonElement dataLocationCode))
                        {
                            if (dataLocationCode.ValueKind == JsonValueKind.String && !string.IsNullOrEmpty(dataLocationCode.GetString()))
                            {
                                if (dataLocations == null)
                                {
                                    dataLocations = new List<IGeoLocationInformation>();
                                }

                                var geoLocation = new GeoLocationInformation()
                                {
                                    DataLocationCode = (GeoLocation)Enum.Parse(typeof(GeoLocation), dataLocationCode.GetString()),
                                    SharePointPortalUrl = siteInformation.GetProperty("webUrl").GetString(),
                                    SharePointAdminUrl = SharePointAdmin.GetTenantAdminCenterUriForStandardTenants(new Uri(siteInformation.GetProperty("webUrl").GetString())).ToString(),
                                    SharePointMySiteUrl = SharePointAdmin.GetTenantMySiteHostUriForStandardTenants(new Uri(siteInformation.GetProperty("webUrl").GetString())).ToString()
                                };

                                if (dataLocations.FirstOrDefault(p => p.DataLocationCode == geoLocation.DataLocationCode) == null)
                                {
                                    dataLocations.Add(geoLocation);
                                }
                            }
                        }
                    }
                }
            }

            return dataLocations;
        }

        public List<IGeoLocationInformation> GetMultiGeoLocations()
        {
            return GetMultiGeoLocationsAsync().GetAwaiter().GetResult();
        }
        #endregion

        #region Access token related extensions
        /// <summary>
        /// Checks if the current access token holds the requested role
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True if the token has the role, false otherwise</returns>
        public async Task<bool> AccessTokenHasRoleAsync(string role)
        {
            return await context.AccessTokenHasRoleAsync(role).ConfigureAwait(false);
        }

        /// <summary>
        /// Checks if the current access token holds the requested role
        /// </summary>
        /// <param name="role">Role to check for</param>
        /// <returns>True if the token has the role, false otherwise</returns>
        public bool AccessTokenHasRole(string role)
        {
            return AccessTokenHasRoleAsync(role).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks if the provided access token holds the requested role
        /// </summary>
        /// <param name="accessToken">Accesstoken to inspect</param>
        /// <param name="role">Role to check for</param>
        /// <returns>True if the token has the role, false otherwise</returns>
        public bool AccessTokenHasRole(string accessToken, string role)
        {
            return PnPContext.AccessTokenHasRole(accessToken, role);
        }

        /// <summary>
        /// Checks if the current access token holds the requested scope
        /// </summary>
        /// <param name="scope">Scopee to check for</param>
        /// <returns>True if the token has the scope, false otherwise</returns>
        public async Task<bool> AccessTokenHasScopeAsync(string scope)
        {
            return await context.AccessTokenHasScopeAsync(scope).ConfigureAwait(false);
        }

        /// <summary>
        /// Checks if the current access token holds the requested scope
        /// </summary>
        /// <param name="scope">Scope to check for</param>
        /// <returns>True if the token has the scope, false otherwise</returns>
        public bool AccessTokenHasScope(string scope)
        {
            return AccessTokenHasScopeAsync(scope).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks if the provided access token holds the requested scope
        /// </summary>
        /// <param name="accessToken">Accesstoken to inspect</param>
        /// <param name="scope">Scope to check for</param>
        /// <returns>True if the token has the scope, false otherwise</returns>
        public bool AccessTokenHasScope(string accessToken, string scope)
        {
            return PnPContext.AccessTokenHasScope(accessToken, scope);
        }

        /// <summary>
        /// Checks if the current access token uses application permissions
        /// </summary>
        /// <returns>True if using application permissions, false otherwise</returns>
        public async Task<bool> AccessTokenUsesApplicationPermissionsAsync()
        {
            return await context.AccessTokenUsesApplicationPermissionsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks if the current access token uses application permissions
        /// </summary>
        /// <returns>True if using application permissions, false otherwise</returns>
        public bool AccessTokenUsesApplicationPermissions()
        {
            return AccessTokenUsesApplicationPermissionsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Checks if the provided access token uses application permissions
        /// </summary>
        /// <param name="accessToken">Accesstoken to inspect</param>
        /// <returns>True if using application permissions, false otherwise</returns>
        public bool AccessTokenUsesApplicationPermissions(string accessToken)
        {
            return PnPContext.AccessTokenUsesApplicationPermissions(accessToken);
        }
        #endregion

    }
}
