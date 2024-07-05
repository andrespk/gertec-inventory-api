CREATE DATABASE IF NOT EXISTS gertec_db;

CREATE USER IF NOT EXISTS 'app_user'@'*';

GRANT SELECT, INSERT, UPDATE, EXECUTE ON gertec_db.* TO 'app_user'@'*';

CREATE TABLE IF NOT EXISTS gertec_db.items (
                                               id UUID PRIMARY KEY,
                                               part_no VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    UNIQUE (part_no)
    );

CREATE TABLE IF NOT EXISTS gertec_db.items_transactions (
                                                            id UUID PRIMARY KEY,
                                                            item_id UUID,
                                                            transaction_date DATETIME,
                                                            transaction_type INT,
                                                            quantity DECIMAL(10, 4),
    part_no DECIMAL(10, 4),
    unit_price DECIMAL(10, 4),
    FOREIGN KEY (item_id) REFERENCES gertec_db.items(id) ON UPDATE CASCADE ON DELETE RESTRICT,
    INDEX (part_no, transaction_date)
    );

CREATE TABLE IF NOT EXISTS gertec_db.daily_inventory (
                                                         id UUID PRIMARY KEY,
                                                         item_id UUID,
                                                         inventory_date DATETIME,
                                                         quantity DECIMAL(10,4),
    part_no DECIMAL(10, 4),
    avg_unit_price DECIMAL(10, 4),
    FOREIGN KEY (item_id) REFERENCES gertec_db.items(id) ON UPDATE CASCADE ON DELETE RESTRICT,
    INDEX (part_no, inventory_date)
    );