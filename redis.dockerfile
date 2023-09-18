FROM redis:alpine

RUN -d -p 6379:6379 -v <redis-data>:/data --name redis dockerfile/redis

VOLUME /data

EXPOSE 6379

CMD ["redis-server"]