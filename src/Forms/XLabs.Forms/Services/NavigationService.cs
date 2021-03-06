﻿// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : Shawn Anderson
// Created          : 12-29-2014
//
// Last Modified By : Shawn Anderson
// Last Modified On : 12-29-2014
// ***********************************************************************
// <copyright file="NavigationService.cs" company="">
//     Copyright (c) 2014 . All rights reserved.
// </copyright>
// <summary>
//	Note: This implementatio is based on the excellent work done in MVVM Light
// </summary>
// ***********************************************************************
namespace XLabs.Forms.Services
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	using XLabs.Platform.Services;

	/// <summary>
	/// Class NavigationService.
	/// </summary>
	public class NavigationService : INavigationService
	{
		/// <summary>
		/// The _page lookup
		/// </summary>
		private readonly IDictionary<string, Type> _pageLookup = new Dictionary<string, Type>();

		/// <summary>
		/// Registers the page (this must be called if you want to use Navigation by pageKey).
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <param name="pageType">Type of the page.</param>
		/// <exception cref="System.ArgumentException">That pagekey is already registered;pageKey</exception>
		public void RegisterPage(string pageKey, Type pageType)
		{
			if (this._pageLookup.ContainsKey(pageKey))
			{
				throw new ArgumentException("That pagekey is already registered", "pageKey");
			}

			this._pageLookup[pageKey] = pageType;
		}

		/// <summary>
		/// Navigates to.
		/// </summary>
		/// <param name="pageKey">The page key.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <exception cref="System.ArgumentException">That pagekey is not registered;pageKey</exception>
		public void NavigateTo(string pageKey, object parameter = null, bool animated = true)
		{
			if (!this._pageLookup.ContainsKey(pageKey))
			{
				throw new ArgumentException("That pagekey is not registered", "pageKey");
			}

			var pageType = this._pageLookup[pageKey];

			this.NavigateTo(pageType, parameter, animated);
		}

		/// <summary>
		/// Navigates to.
		/// </summary>
		/// <param name="pageType">Type of the page.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <exception cref="System.ArgumentException">Argument must be derived from type Xamarin.Forms.Page;pageType</exception>
		public void NavigateTo(Type pageType, object parameter = null, bool animated = true)
		{
			var p = Activator.CreateInstance(pageType);

			if (p == null || !p.GetType().GetTypeInfo().IsAssignableFrom(typeof(Xamarin.Forms.Page).GetTypeInfo()))
			{
				throw new ArgumentException("Argument must be derived from type Xamarin.Forms.Page", "pageType");
			}

			var nav = Xamarin.Forms.DependencyService.Get<Xamarin.Forms.INavigation>();

			nav.PushAsync((Xamarin.Forms.Page)p, animated).Start();
		}

		/// <summary>
		/// Navigates to.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameter">The parameter.</param>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <exception cref="System.ArgumentException">Page Type must be based on Xamarin.Forms.Page</exception>
		public void NavigateTo<T>(object parameter = null, bool animated = true) where T : class
		{
			var p = Activator.CreateInstance(typeof(T));

			if (p == null || !p.GetType().GetTypeInfo().IsAssignableFrom(typeof(Xamarin.Forms.Page).GetTypeInfo()))
			{
				throw new ArgumentException("Page Type must be based on Xamarin.Forms.Page");
			}

			var nav = Xamarin.Forms.DependencyService.Get<Xamarin.Forms.INavigation>();

			nav.PushAsync((Xamarin.Forms.Page)p, animated).Start();
		}

		/// <summary>
		/// Goes back.
		/// </summary>
		public void GoBack()
		{
			var nav = Xamarin.Forms.DependencyService.Get<Xamarin.Forms.INavigation>();

			nav.PopAsync(true).Start();
		}

		/// <summary>
		/// Goes forward.
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void GoForward()
		{
#if DEBUG
			throw new NotImplementedException();
#endif
		}
	}
}