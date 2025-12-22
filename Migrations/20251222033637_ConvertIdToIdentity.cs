using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectDetails.Migrations
{
    /// <inheritdoc />
    public partial class ConvertIdToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Safe conversion: reassign all Ids using a sequence
-- Drop temp column if it exists from previous failed attempt
ALTER TABLE ""paymentDetails"" DROP COLUMN IF EXISTS ""_new_id"";

CREATE SEQUENCE IF NOT EXISTS paymentdetails_id_seq;

-- Add temp bigint column
ALTER TABLE ""paymentDetails"" ADD COLUMN ""_new_id"" bigint;

-- Assign new sequential bigint Ids from the sequence
UPDATE ""paymentDetails""
SET ""_new_id"" = nextval('paymentdetails_id_seq');

-- Set sequence to max(new_id)
SELECT setval('paymentdetails_id_seq', (SELECT COALESCE(MAX(""_new_id""), 1) FROM ""paymentDetails""));

-- Drop old Id column and rename temp to Id
ALTER TABLE ""paymentDetails"" DROP COLUMN ""Id"";
ALTER TABLE ""paymentDetails"" RENAME COLUMN ""_new_id"" TO ""Id"";

-- Make Id NOT NULL and set default to nextval
ALTER TABLE ""paymentDetails"" ALTER COLUMN ""Id"" SET NOT NULL;
ALTER TABLE ""paymentDetails"" ALTER COLUMN ""Id"" SET DEFAULT nextval('paymentdetails_id_seq');

-- Own the sequence
ALTER SEQUENCE paymentdetails_id_seq OWNED BY ""paymentDetails"".""Id"";
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Rollback: remove defaults/sequence (cannot restore original values safely)
ALTER TABLE ""paymentDetails"" ALTER COLUMN ""Id"" DROP DEFAULT;
DROP SEQUENCE IF EXISTS paymentdetails_id_seq CASCADE;
            ");
        }
    }
}
