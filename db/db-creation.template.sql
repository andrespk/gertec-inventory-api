CREATE DATABASE IF NOT EXISTS ${DB_NAME};

CREATE USER IF NOT EXISTS '${DB_USER}'@'localhost' IDENTIFIED BY '${DB_PASSWORD}';

GRANT SELECT, INSERT, UPDATE, EXECUTE ON ${DB_NAME}.* TO '${DB_USER}'@'localhost';

CREATE TABLE IF NOT EXISTS ${DB_NAME}.items (
id BINARY(16) PRIMARY KEY,
part_no VARCHAR(10) NOT NULL,
description VARCHAR(255),
UNIQUE (part_no)
);

CREATE TABLE IF NOT EXISTS ${DB_NAME}.items_transactions (
id BINARY(16) PRIMARY KEY,
item_id BINARY(16),
transaction_date DATETIME,
inventory_date DATETIME,
transaction_type INT,
quantity DECIMAL(10, 4),
amount DECIMAL(10, 2),
unit_price DECIMAL(10, 4),
FOREIGN KEY (item_id) REFERENCES ${DB_NAME}.items(id) ON UPDATE CASCADE ON DELETE RESTRICT,
INDEX (item_id, transaction_date)
);

CREATE TABLE IF NOT EXISTS ${DB_NAME}.daily_inventory (
id BINARY(16) PRIMARY KEY,
item_id BINARY(16),
inventory_date DATETIME,
quantity DECIMAL(10,4),
amount DECIMAL(10, 2),
avg_unit_price DECIMAL(10, 4),
FOREIGN KEY (item_id) REFERENCES ${DB_NAME}.items(id) ON UPDATE CASCADE ON DELETE RESTRICT,
INDEX (item_id, inventory_date)
);

CREATE TABLE IF NOT EXISTS ${DB_NAME}.application_logs (
Id INT AUTO_INCREMENT,
Timestamp DATETIME NOT NULL,
Level VARCHAR(20) NOT NULL,
Message TEXT NOT NULL,
Source VARCHAR(2000) NOT NULL,
Exception TEXT,
Request TEXT,
ProblemDetails TEXT
);