dotnet run --project ProLearning.Api &
sleep 5
curl http://localhost:5075/openapi/v1.json -o openapi.json
widdershins openapi.json -o api.md --language_tabs 'http:HTTP' 'csharp:C#' --omitHeader
kill %1
echo "Docs generated at api.md"