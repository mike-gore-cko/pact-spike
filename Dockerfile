FROM postgres:15rc1-alpine

# See docs on initalization scripts here https://hub.docker.com/_/postgres
ADD init.sql /docker-entrypoint-initdb.d/