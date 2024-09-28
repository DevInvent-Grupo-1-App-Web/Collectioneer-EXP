USE prod;

-- Create triggers for updating the updated_at column on ITimestamped entities

DROP TRIGGER IF EXISTS update_role;
CREATE TRIGGER update_role
BEFORE UPDATE ON roles
FOR EACH ROW
SET NEW.updated_at = NOW();


DROP TRIGGER IF EXISTS update_auction;
CREATE TRIGGER update_auction
BEFORE UPDATE ON auctions
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_exchange;
CREATE TRIGGER update_exchange
BEFORE UPDATE ON exchanges
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_sale;
CREATE TRIGGER update_sale
BEFORE UPDATE ON sales
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_article;
CREATE TRIGGER update_article
BEFORE UPDATE ON articles
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_collectible;
CREATE TRIGGER update_collectible
BEFORE UPDATE ON collectibles
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_review;
CREATE TRIGGER update_review
BEFORE UPDATE ON reviews
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_bid;
CREATE TRIGGER update_bid
BEFORE UPDATE ON bids
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_media_element;
CREATE TRIGGER update_media_element
BEFORE UPDATE ON media_elements
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_post;
CREATE TRIGGER update_post
BEFORE UPDATE ON posts
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_comment;
CREATE TRIGGER update_comment
BEFORE UPDATE ON comments
FOR EACH ROW
SET NEW.updated_at = NOW();

DROP TRIGGER IF EXISTS update_reaction;
CREATE TRIGGER update_reaction
BEFORE UPDATE ON reactions
FOR EACH ROW
SET NEW.updated_at = NOW();