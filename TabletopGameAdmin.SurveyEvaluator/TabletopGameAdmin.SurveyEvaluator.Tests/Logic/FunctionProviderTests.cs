﻿namespace TabletopGameAdmin.SurveyEvaluator.Tests.Logic
{
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using TabletopGameAdmin.SurveyEvaluator.Contracts;
	using TabletopGameAdmin.SurveyEvaluator.Logic;
	using TabletopGameAdmin.SurveyEvaluator.Model;
	using Xunit;

	/// <summary>
	///   Tests for <see cref="FunctionProvider" />
	/// </summary>
	public class FunctionProviderTests
	{
		/// <summary>
		///   Throw <see cref="ArgumentNullException" /> if the input is null.
		/// </summary>
		[Fact]
		public async void HandleAsync_Null_ArgumentNullException()
		{
			var provider = await this.InitAsync();
			await Assert.ThrowsAsync<ArgumentNullException>(() => provider.HandleAsync(null));
		}

		/// <summary>
		///   Initialize the provider and its dependencies.
		/// </summary>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IFunctionProvider" />.</returns>
		private async Task<IFunctionProvider> InitAsync()
		{
			var configuration =
				JsonConvert.DeserializeObject<FunctionConfiguration>(
					await File.ReadAllTextAsync("appsettings.Development.json"));
			var provider = new FunctionProvider(configuration);
			return provider;
		}
	}
}