﻿using System;
using System.Net.Http;

namespace WeText.Common.Consul
{
    /// <summary>
    /// Consul RsetApi客户端
    /// </summary>
    public partial class ConsulRsetApiClient : IConsulRsetApiClient
    {
        private HttpClient _httpClient;

        private Uri _apiBaseUri;        

        #region 构造函数
        public ConsulRsetApiClient()
            : this(ConsulConstants.DefaultApiBaseUri)
        { }

        public ConsulRsetApiClient(Uri apiBaseUri)
        {
            _apiBaseUri = apiBaseUri ?? ConsulConstants.DefaultApiBaseUri;
            _httpClient = new HttpClient() { BaseAddress = _apiBaseUri };            
        }
        #endregion

        #region 属性
        protected HttpClient httpClient { get { return _httpClient; } }
        #endregion

        #region IDisposable实现
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
