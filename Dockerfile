FROM mono:6.8

COPY ./Solution/InputOutput.cs /app/
COPY ./Solution/Program.cs /app/
COPY ./Tests/Samples/1.in /app/samples/

RUN dmcs -out:/app/app.exe -optimize+ -r:System.Numerics /app/*.cs

CMD [ "/bin/bash", "-c", "cat /app/samples/1.in | mono /app/app.exe"]