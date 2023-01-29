REM Schema validator (https://github.com/ajv-validator/ajv and ajv-cli)
echo x

ajv -s ..\gaom.schema.json -d .\simple_service.gaom.yaml --spec=draft2020
ajv -s ..\gaom.schema.json -d .\webapp.gaom.yaml --spec=draft2020
ajv -s ..\gaom.schema.json -d .\webapp_with_local_proxy.gaom.yaml --spec=draft2020

REM Document generator (https://github.com/CesiumGS/wetzel)
npx wetzel ..\gaom.schema.json > ..\gaom.schema.md