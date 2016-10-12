﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coolector.Dto.Remarks;
using Coolector.Tests.EndToEnd.Framework;
using FluentAssertions;
using Machine.Specifications;

namespace Coolector.Tests.EndToEnd.API.Modules
{
    public abstract class RemarksModule_specs : ModuleBase_specs
    {
        protected static RemarkDto GetRemark(Guid id)
            => HttpClient.GetAsync<RemarkDto>($"remarks/{id}").WaitForResult();

        protected static IEnumerable<RemarkDto> GetLatestRemarks()
            => HttpClient.GetCollectionAsync<RemarkDto>("remarks?latest=true").WaitForResult();

        protected static IEnumerable<RemarkCategoryDto> GetCategories()
            => HttpClient.GetCollectionAsync<RemarkCategoryDto>("remarks/categories").WaitForResult();

        protected static Stream GetPhoto(Guid id)
            => HttpClient.GetStreamAsync($"remarks/{id}/photo").WaitForResult();
    }

    [Subject("Remarks collection")]
    public class when_fetching_latest_remarks : RemarksModule_specs
    {
        static IEnumerable<RemarkDto> Remarks;

        Establish context = () => Initialize();

        Because of = () => Remarks = GetLatestRemarks();

        It should_return_non_empty_collection = () =>
        {
            Remarks.ShouldNotBeEmpty();
            foreach (var remark in Remarks)
            {
                remark.Id.ShouldNotEqual(Guid.Empty);
                remark.Author.UserId.ShouldNotBeEmpty();
                remark.Author.Name.ShouldNotBeEmpty();
                remark.Category.Id.ShouldNotEqual(Guid.Empty);
                remark.Category.Name.ShouldNotBeEmpty();
                remark.Location.Coordinates.Length.ShouldEqual(2);
                remark.Location.Coordinates[0].ShouldNotEqual(0);
                remark.Location.Coordinates[1].ShouldNotEqual(0);
                remark.Photo.Name.ShouldNotBeEmpty();
                remark.Photo.FileId.ShouldNotBeEmpty();
                remark.Photo.ContentType.ShouldNotBeEmpty();
            }
        };
    }

    [Subject("Remark details")]
    public class when_fetching_remark : RemarksModule_specs
    {
        static IEnumerable<RemarkDto> Remarks;
        static RemarkDto SelectedRemark;
        static RemarkDto Remark;
        static Stream Photo;

        Establish context = () => Initialize();

        Because of = () =>
        {
            Remarks = GetLatestRemarks();
            SelectedRemark = Remarks.First();
            Remark = GetRemark(SelectedRemark.Id);
            Photo = GetPhoto(SelectedRemark.Id);
        };

        It should_return_remark = () =>
        {
            Remark.ShouldNotBeNull();
            Remark.Id.ShouldBeEquivalentTo(SelectedRemark.Id);
            Remark.Category.Id.ShouldBeEquivalentTo(SelectedRemark.Category.Id);
            Remark.Author.UserId.ShouldBeEquivalentTo(SelectedRemark.Author.UserId);
            Remark.Description.ShouldBeEquivalentTo(SelectedRemark.Description);
            Remark.Photo.FileId.ShouldBeEquivalentTo(SelectedRemark.Photo.FileId);
        };

        It should_have_photo = () =>
        {
            Photo.ShouldNotBeNull();
            Photo.CanRead.ShouldBeTrue();
        };
    }

    [Subject("Remarks categories")]
    public class when_fetching_remarks_categories : RemarksModule_specs
    {
        static IEnumerable<RemarkCategoryDto> Categories;

        Establish context = () => Initialize(authenticate: false);

        Because of = () => Categories = GetCategories();

        It should_return_non_empty_collection = () =>
        {
            Categories.ShouldNotBeEmpty();
            foreach (var category in Categories)
            {
                category.Id.ShouldNotEqual(Guid.Empty);
                category.Name.ShouldNotBeEmpty();
            }
        };
    }
}