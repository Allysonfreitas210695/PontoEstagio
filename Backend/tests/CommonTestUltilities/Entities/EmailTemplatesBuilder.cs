using System;
using Bogus;
using PontoEstagio.Domain.Entities;

namespace CommonTestUltilities.Entities;

public static class EmailTemplateBuilder
{
    public static EmailTemplates Build(
        Guid? id = null,
        string? title = null,
        string? subject = null,
        string? body = null
    )
    {
        var faker = new Faker();

        return new EmailTemplates(
            id: id ?? Guid.NewGuid(),
            title: title ?? faker.Lorem.Sentence(2),
            subject: subject ?? faker.Lorem.Sentence(),
            body: body ?? faker.Lorem.Paragraph()
        );
    }
}
